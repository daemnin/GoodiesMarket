using GoodiesMarket.Components.Models;
using GoodiesMarket.Components.Proxies;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace GoodiesMarket.Business.Proxies
{
    public class SecurityProxy : ProxyBase
    {
        private readonly string Controller = "Account";

        public async Task<Result> Register(string email, string password, RoleType roleType)
        {
            var requestUri = $"{Controller}/Register";

            var request = new
            {
                email = email,
                password = password,
                roleType = roleType
            };

            var body = JsonConvert.SerializeObject(request);

            using (var httpClient = GetSecureClient())
            {
                var result = await httpClient.Post(requestUri, body);

                return result;
            }
        }
    }
}