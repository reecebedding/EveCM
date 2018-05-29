using EveCM.Managers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using EveCM.Models;

namespace EveCM.Managers
{
    public class OAuthManager : IOAuthManager
    {
        private readonly EveSettings _eveSettings;
        
        public OAuthManager(IOptions<EveSettings> eveSettings)
        {
            _eveSettings = eveSettings.Value;
        }

        public string EVERedirectUrl()
        {
            string url = $"{_eveSettings.SSO.LoginHost}/authorize";
            url = AppendQueryParams(url,
                ("response_type", _eveSettings.SSO.ResponseType),
                ("redirect_uri", _eveSettings.SSO.CallBackPath),
                ("client_id", _eveSettings.SSO.ClientId),
                ("scope", string.Empty)
            );

            return url;
        }

        public CharacterDetails GetCharacterDetailsFromCode(string code)
        {
            TokenRequestResult token = GetUserToken(code);
            return GetCharacterDetailsFromToken(token.AccessToken);
        }

        public CharacterDetails GetCharacterDetailsFromToken(string token)
        {
            string url = $"{_eveSettings.SSO.LoginHost}/verify";

            HttpClient client = GetOAuthClient((AuthenticationType.Bearer, token));
            
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;

                CharacterDetails character = JsonConvert.DeserializeObject<CharacterDetails>(responseBody);
                return character;
            }

            return null;
        }

        public TokenRequestResult GetUserToken(string code)
        {
            string url = $"{_eveSettings.SSO.LoginHost}/token";

            HttpContent requestBody = new StringContent(
                JsonConvert.SerializeObject(new { grant_type = "authorization_code", code }),
                Encoding.UTF8,
                "application/json"
            );

            HttpClient client = GetOAuthClient((AuthenticationType.Basic, $"{_eveSettings.SSO.ClientId}:{_eveSettings.SSO.ClientSecret}"));
            
            HttpResponseMessage response = client.PostAsync(url, requestBody).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;

                TokenRequestResult result = JsonConvert.DeserializeObject<TokenRequestResult>(responseBody);
                return result;
            }

            return null;
        }

        private HttpClient GetOAuthClient((AuthenticationType type, string value) authenticationMode)
        {
            HttpClient client = new HttpClient();

            if (authenticationMode.type == AuthenticationType.Basic)
                authenticationMode.value = Convert.ToBase64String(Encoding.UTF8.GetBytes(authenticationMode.value));

            if (!string.IsNullOrEmpty(authenticationMode.value))
                client.DefaultRequestHeaders.Add("Authorization", $"{authenticationMode.type} {authenticationMode.value}");

            return client;
        }

        private enum AuthenticationType { Bearer, Basic }

        private string AppendQueryParams(string baseUrl, IEnumerable<(string name, string value)> queryParams)
        {
            string fullUri = baseUrl;
            void appendParam(string name, string value)
            {
                char seperator = '?';
                if (!string.Equals(baseUrl, fullUri))
                    seperator = '&';

                fullUri += $"{seperator}{name}={value}";
            }

            queryParams.ToList().ForEach(param =>
            {
                if (!string.IsNullOrEmpty(param.name) && !string.IsNullOrEmpty(param.value))
                    appendParam(param.name, param.value);
            });

            return fullUri;
        }
        private string AppendQueryParams(string baseUrl, params (string, string)[] queryParams) => AppendQueryParams(baseUrl, queryParams.ToList());
    }
}
