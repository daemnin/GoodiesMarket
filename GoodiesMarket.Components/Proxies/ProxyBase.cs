using GoodiesMarket.Components.Configs;
using GoodiesMarket.Components.Contracts;
using GoodiesMarket.Components.Http;
using System;

namespace GoodiesMarket.Components.Proxies
{
    public abstract class ProxyBase
    {
        protected IHttpClient GetClient(ICredentials credentials = null, params Tuple<string, string>[] headers)
        {
            return new GenericHttpClient(Constants.API_BASEADDRESS, credentials, headers);
        }

        protected IHttpClient GetSecureClient(ICredentials credentials = null, params Tuple<string, string>[] headers)
        {
            return new GenericHttpClient(Constants.SECURITY_API_BASEADDRESS, credentials, headers);
        }
    }
}
