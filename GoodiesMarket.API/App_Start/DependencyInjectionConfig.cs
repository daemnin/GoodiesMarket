using GoodiesMarket.Data;
using GoodiesMarket.Data.Contracts;
using GoodiesMarket.Model;
using GoodiesMarket.Model.Contracts;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.OwinHost;
using Owin;
using System.Reflection;

namespace GoodiesMarket.API
{
    public class DependencyInjectionConfig
    {
        public static void Register(IAppBuilder app)
        {
            app.UseNinjectMiddleware(CreateKernel);
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            RegisterServices(kernel);
            return kernel;
        }

        private static void RegisterServices(StandardKernel kernel)
        {
            kernel.Bind<IContext>().To<GoodiesMarketContext>().InRequestScope();

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}