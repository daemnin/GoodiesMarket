using GoodiesMarket.App.Views;
using Prism.Ninject;
using Xamarin.Forms;

namespace GoodiesMarket.App
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            var isSeller = false;
            if (isSeller)
            {
                NavigationService.NavigateAsync("NavigationPage/SellerMasterPage/SellerProfile?title=Hello, I'm a seller");
            }
            else
            {
                NavigationService.NavigateAsync("NavigationPage/BuyerMasterPage/BuyerProfile?title=Hello, I'm a buyer");
            }
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<SellerMasterPage>();
            Container.RegisterTypeForNavigation<SellerProfile>();
            Container.RegisterTypeForNavigation<BuyerMasterPage>();
            Container.RegisterTypeForNavigation<BuyerProfile>();
        }
    }
}
