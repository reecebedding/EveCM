﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EveCM.Data;
using EveCM.Data.Repositories.Contracts;
using EveCM.Data.Repositories.PSQL;
using EveCM.Managers;
using EveCM.Managers.Admin;
using EveCM.Managers.Admin.Contracts;
using EveCM.Managers.Bulletin;
using EveCM.Managers.Bulletin.Contracts;
using EveCM.Managers.Profile;
using EveCM.Managers.Profile.Contracts;
using EveCM.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
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
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<EveCMContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("EveCM")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<EveCMContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/auth/login";
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -._@+";
            });

            var eveSection = Configuration.GetSection("EVE");
            services.Configure<EveSettings>(eveSection);

            RegisterDIBindings(services);

            services.AddMemoryCache();
            services.AddSession();
            services.AddAuthorization();
            services.AddMvc();
            services.AddAutoMapper();
        }

        private void RegisterDIBindings(IServiceCollection services)
        {
            services.AddTransient<IOAuthManager, OAuthManager>();
            services.AddTransient<IProfileManager, ProfileManager>();
            services.AddTransient<IBulletinManager, BulletinManager>();
            services.AddTransient<IAdminManager, AdminManager>();

            services.AddTransient<ICharacterRepository, CharacterRepository>();
            services.AddTransient<IBulletinRepository, BulletinRepository>();

            services.AddTransient<EveCMContextSeeder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, EveCMContextSeeder contextSeeder)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            contextSeeder.Seed();
        }
    }
}
