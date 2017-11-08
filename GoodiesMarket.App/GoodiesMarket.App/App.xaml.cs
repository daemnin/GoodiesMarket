using GoodiesMarket.App.Models;
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

            var userType = UserType.Unidentified;

            switch (userType)
            {
                case UserType.Unidentified:
                    NavigationService.NavigateAsync("Login");
                    break;
                case UserType.Seller:
                    NavigationService.NavigateAsync($"NavigationPage/SellerMasterPage/SellerProfile");
                    break;
                case UserType.Buyer:
                    NavigationService.NavigateAsync("NavigationPage/BuyerMasterPage/BuyerProfile?title=Hello, I'm a buyer");
                    break;
            }
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<SellerMasterPage>();
            Container.RegisterTypeForNavigation<SellerProfile>();
            Container.RegisterTypeForNavigation<BuyerMasterPage>();
            Container.RegisterTypeForNavigation<BuyerProfile>();
            Container.RegisterTypeForNavigation<RegistrationWizard>();
            Container.RegisterTypeForNavigation<RegistrationName>();
            Container.RegisterTypeForNavigation<Login>();
            Container.RegisterTypeForNavigation<RegistrationEmail>();
            Container.RegisterTypeForNavigation<RegistrationPassword>();
            Container.RegisterTypeForNavigation<AddProduct>();
            Container.RegisterTypeForNavigation<PlaceOrder>();
        }
    }
}
