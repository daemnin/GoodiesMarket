using GoodiesMarket.Components.Helpers;
using GoodiesMarket.Components.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace GoodiesMarket.Components.Proxies
{
    public class ProductProxy : ProxyBase
    {
        private const string Controller = "Product";

        public async Task<Result> Create(string name, string description, int? stock, float price)
        {
            var requestUri = $"{Controller}";

            var request = new
            {
                name,
                description,
                stock,
                price
            };

            var body = JsonConvert.SerializeObject(request);

            using (var client = GetClient(Credentials.Instance))
            {
                var result = await client.Post(requestUri, body);

                return result;
            }
        }

        public async Task<Result> Delete(long id)
        {
            var requestUri = $"{Controller}/{id}";

            using (var client = GetClient(Credentials.Instance))
            {
                var result = await client.Delete(requestUri);

                return result;
            }
        }

        public async Task<Result> Update(long id, string name, string description, int? stock, float price)
        {
            var requestUri = $"{Controller}/{id}";

            var request = new
            {
                name,
                description,
                stock,
                price
            };

            var body = JsonConvert.SerializeObject(request);

            using (var client = GetClient(Credentials.Instance))
            {
                var result = await client.Put(requestUri, body);

                return result;
            }
        }
    }
}
