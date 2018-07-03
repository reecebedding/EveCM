using EveCM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM.Managers
{
    public interface IOAuthManager
    {
        string EVERedirectUrl();
        TokenRequestResult GetUserToken(string code);
        CharacterDetails GetCharacterDetailsFromToken(string token);
        CharacterDetails GetCharacterDetailsFromCode(string code);
    }

    public class TokenRequestResult
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
