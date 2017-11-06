using GoodiesMarket.Data;
using GoodiesMarket.Data.Contracts;
using GoodiesMarket.Model;
using GoodiesMarket.Model.Contracts;
using Ninject;
using Ninject.Web.Common;
using System.Reflection;

namespace GoodiesMarket.API
{
    internal class NinjectConfig
    {
        public static IKernel Register()
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