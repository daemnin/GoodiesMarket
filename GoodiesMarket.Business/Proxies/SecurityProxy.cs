using GoodiesMarket.Components.Http;
using GoodiesMarket.Components.Proxies;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace GoodiesMarket.Business.Proxies
{
    public class SecurityProxy : ProxyBase
    {
        private readonly string Controller = "Account";

        public async Task<BaseResponse> Register(string email, string password, bool isSeller)
        {
            var requestUri = $"{Controller}/Register";

            var request = new
            {
                email = email,
                password = password,
                roleType = isSeller ? 1 : 0
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