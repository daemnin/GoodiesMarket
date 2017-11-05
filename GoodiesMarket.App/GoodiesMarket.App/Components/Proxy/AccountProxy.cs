using GoodiesMarket.App.Components.Http;
using GoodiesMarket.App.Configs;
using GoodiesMarket.App.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoodiesMarket.App.Components.Proxies
{
    public class AccountProxy : ProxyBase
    {
        private readonly string Controller = "Account";

        public async Task<GenericResponse> SignIn(LoginModel model)
        {
            var requestUri = $"{Controller}/Login";

            var request = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", model.Email),
                new KeyValuePair<string, string>("password", model.Password),
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("client_id", Constants.CLIENT_ID)
            };

            var body = await new FormUrlEncodedContent(request).ReadAsStringAsync();

            using (var httpClient = GetSecureClient())
            {
                var response = await httpClient.Post(requestUri, body, "application/x-www-form-urlencoded");

                return response;
            }
        }

        public async Task<GenericResponse> Register(RegistrationModel model)
        {
            var requestUri = $"{Controller}/Register";

            var request = new
            {
                username = model.Email,
                password = model.Password,
                roleType = model.IsSeller ? 1 : 0
            };

            var body = JsonConvert.SerializeObject(request);

            using (var httpClient = GetClient())
            {
                var response = await httpClient.Post(requestUri, body);

                return response;
            }
        }
    }
}