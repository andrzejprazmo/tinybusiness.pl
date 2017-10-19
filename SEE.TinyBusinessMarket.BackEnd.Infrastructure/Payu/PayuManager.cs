//-----------------------------------------------------------------------
// <copyright file="PayuRepository.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Infrastructure.Payu
{
    using Microsoft.Extensions.Options;
    using SEE.TinyBusinessMarket.BackEnd.Common.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using SEE.TinyBusinessMarket.BackEnd.Common.Contract;
    using SEE.Framework.Core.Common;
    using SEE.TinyBusinessMarket.BackEnd.Common.Consts;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class PayuManager : IPayuManager
    {
        private readonly PayuConfiguration _payuConfiguration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PayuManager(IOptions<PayuConfiguration> options, IHttpContextAccessor httpContextAccessor)
        {
            _payuConfiguration = options.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CommandResult<PayuOutputContract>> CreateOrderAsync(string token, PayuProductContract product, PayuBuyerContract buyer)
        {
            var connection = _httpContextAccessor.HttpContext.Connection;
            var request = _httpContextAccessor.HttpContext.Request;
            string baseUrl = $"{request.Scheme}://{request.Host}";
            if (!string.IsNullOrWhiteSpace(request.PathBase))
            {
                baseUrl = $"{baseUrl}/{request.PathBase}";
            }
            var payuInput = new PayuInputContract
            {
                NotifyUrl = $"{baseUrl}/Payment/Finish?token={token}",
                CustomerIp = connection.RemoteIpAddress.ToString(),
                MerchantPosId = _payuConfiguration.PosId,
                CurrencyCode = PayuConsts.CurrencyCode,
                Description = PayuConsts.Description,
                Settings = new PayuSettingsContract
                {
                    InvoiceDisabled = true,
                },
                TotalAmount = product.UnitPrice,
                Buyer = buyer,
                Products = new List<PayuProductContract> { product }
            };
            var oAuth = await GetSecretAsync();
            var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            string httpData = JsonConvert.SerializeObject(payuInput, Formatting.Indented, jsonSerializerSettings);
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(oAuth.TokenType, oAuth.AccessToken);
                var httpContent = new StringContent(httpData, Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync(_payuConfiguration.OrderUrl, httpContent);
                if (result.StatusCode == System.Net.HttpStatusCode.Found)
                {
                    return new CommandResult<PayuOutputContract>(new PayuOutputContract
                    {
                        RedirectUri = result.RequestMessage.RequestUri.AbsoluteUri,
                    });
                }
            }
            return new CommandResult<PayuOutputContract>(new PayuOutputContract { });
        }

        private async Task<OAuthResponse> GetSecretAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var httpContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", _payuConfiguration.PosId),
                    new KeyValuePair<string, string>("client_secret", _payuConfiguration.ClientSecret),
                });
                var result = await httpClient.PostAsync(_payuConfiguration.AuthorizeUrl, httpContent);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = result.Content.ReadAsStringAsync();
                    string data = content.Result;
                    return JsonConvert.DeserializeObject<OAuthResponse>(data);
                }
            }
            return null;
        }
    }
}
