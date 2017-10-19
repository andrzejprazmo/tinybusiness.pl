//-----------------------------------------------------------------------
// <copyright file="OAuthResponse.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Infrastructure.Payu
{
    using Newtonsoft.Json;
    using System;
	using System.Collections.Generic;
	using System.Text;
	

    public class OAuthResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; }
    }
}
