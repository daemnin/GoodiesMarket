using GoodiesMarket.Security.API.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(GoodiesMarket.Security.API.Startup))]

namespace GoodiesMarket.Security.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            IKernel kernel = DependencyInjectionConfig.CreateAndRegister(app);

            ConfiureOAuth(app, kernel);
            WebApiConfig.Register(config);

            app.UseNinjectWebApi(config);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        private void ConfiureOAuth(IAppBuilder app, IKernel kernel)
        {
            var OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/account/login"),
                Provider = new AuthorizationProvider(kernel),
                RefreshTokenProvider = new RefreshTokenProvider(kernel)
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
