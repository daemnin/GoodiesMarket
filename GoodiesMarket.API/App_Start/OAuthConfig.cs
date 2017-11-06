using Microsoft.Owin.Security.OAuth;
using Owin;

namespace GoodiesMarket.API
{
    public class OAuthConfig
    {
        public static void Register(IAppBuilder app)
        {
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}