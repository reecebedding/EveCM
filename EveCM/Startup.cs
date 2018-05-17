using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

namespace EveCM
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "Eve";
            })
        .AddCookie()
        .AddOAuth("Eve", options =>
        {
            Uri callBackPath = new Uri(Configuration["EVE:SSO:CallBackPath"]);

            options.ClientId = Configuration["EVE:SSO:ClientId"];
            options.ClientSecret = Configuration["EVE:SSO:ClientSecret"];
            options.CallbackPath = new PathString(callBackPath.PathAndQuery);

            var responseType = Configuration["EVE:SSO:ResponseType"];
            options.AuthorizationEndpoint = $"https://login.eveonline.com/oauth/authorize?response_type={responseType}&redirect_uri={callBackPath.ToString()}";
            options.TokenEndpoint = new Uri(new Uri(Configuration["EVE:SSO:LoginHost"]), "oauth/token").ToString();
            options.UserInformationEndpoint = new Uri(new Uri(Configuration["EVE:SSO:LoginHost"]), "oauth/verify").ToString();

            options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "CharacterID");
            options.ClaimActions.MapJsonKey(ClaimTypes.Name, "CharacterName");
            options.ClaimActions.MapJsonKey(ClaimTypes.Expiration, "ExpiresOn");

            options.Events = new OAuthEvents
            {
                OnCreatingTicket = async context =>
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

                    var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                    response.EnsureSuccessStatusCode();

                    var user = JObject.Parse(await response.Content.ReadAsStringAsync());

                    context.RunClaimActions(user);
                }
            };
        });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
