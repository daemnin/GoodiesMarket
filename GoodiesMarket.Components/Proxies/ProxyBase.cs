using GoodiesMarket.Components.Configs;
using GoodiesMarket.Components.Contracts;
using GoodiesMarket.Components.Http;
using System;

namespace GoodiesMarket.Components.Proxies
{
    public abstract class ProxyBase
    {
        protected IHttpClient GetClient(bool isAuthorized = false, params Tuple<string, string>[] headers)
        {
            return new GenericHttpClient(Constants.API_BASEADDRESS, isAuthorized, headers);
        }

        protected IHttpClient GetSecureClient(bool isAuthoirzed = false, params Tuple<string, string>[] headers)
        {
            return new GenericHttpClient(Constants.SECURITY_API_BASEADDRESS, isAuthoirzed, headers);
        }
    }
}
