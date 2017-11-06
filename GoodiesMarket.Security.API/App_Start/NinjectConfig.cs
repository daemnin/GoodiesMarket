using GoodiesMarket.Security.Data.Repository;
using GoodiesMarket.Security.Model;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.OwinHost;
using Owin;
using System.Reflection;

namespace GoodiesMarket.Security.API
{
    public class NinjectConfig
    {
        public static IKernel CreateAndRegister(IAppBuilder app)
        {
            IKernel kernel = CreateKernel();
            app.UseNinjectMiddleware(() => { return kernel; });
            return kernel;
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            RegisterServices(kernel);
            return kernel;
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<GoodiesMarketAuthContext>().To<GoodiesMarketAuthContext>().InRequestScope();
            kernel.Bind<ISecurityRepository>().To<SecurityRepository>();
        }
    }
}