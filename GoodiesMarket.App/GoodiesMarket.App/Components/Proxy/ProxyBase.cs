using GoodiesMarket.App.Components.Http;
using GoodiesMarket.App.Configs;
using System;

namespace GoodiesMarket.App.Components.Proxies
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