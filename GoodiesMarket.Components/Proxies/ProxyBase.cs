using GoodiesMarket.Components.Configs;
using GoodiesMarket.Components.Contracts;
using GoodiesMarket.Components.Http;
using System;

namespace GoodiesMarket.Components.Proxies
{
    public abstract class ProxyBase
    {
        protected IHttpClient GetClient(params Tuple<string, string>[] headers)
        {
            return new GenericHttpClient(Constants.API_BASEADDRESS, headers);
        }

        protected IHttpClient GetSecureClient(params Tuple<string, string>[] headers)
        {
            return new GenericHttpClient(Constants.SECURITY_API_BASEADDRESS, headers);
        }
    }
}
