using GoodiesMarket.Components.Configs;
using GoodiesMarket.Components.Contracts;
using GoodiesMarket.Components.Http;

namespace GoodiesMarket.Components.Proxies
{
    public abstract class ProxyBase
    {
        protected IHttpClient GetClient(ICredentials credentials = null)
        {
            return new GenericHttpClient(Constants.API_BASEADDRESS, credentials);
        }

        protected IHttpClient GetSecureClient(ICredentials credentials = null)
        {
            return new GenericHttpClient(Constants.SECURITY_API_BASEADDRESS, credentials);
        }
    }
}
