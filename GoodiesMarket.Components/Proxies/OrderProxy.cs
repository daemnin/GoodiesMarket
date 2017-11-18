using GoodiesMarket.Components.Helpers;
using GoodiesMarket.Components.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodiesMarket.Components.Proxies
{
    public class OrderProxy : ProxyBase
    {
        private const string Controller = "Order";

        public async Task<Result> Get()
        {
            var requestUri = $"{Controller}";

            using (var client = GetClient(Credentials.Instance))
            {
                var result = await client.Get(requestUri);

                return result;
            }
        }

        public async Task<Result> Get(long id)
        {
            var requestUri = $"{Controller}/{id}";

            using (var client = GetClient(Credentials.Instance))
            {
                var result = await client.Get(requestUri);

                return result;
            }
        }

        public async Task<Result> ChangeStatus(long id, StatusType status)
        {
            var requestUri = $"{Controller}/{id}";

            var request = new
            {
                status
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