using GoodiesMarket.App.Views;
using GoodiesMarket.Components.Models;
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

            var userType = RoleType.Unidentified;

            switch (userType)
            {
                case RoleType.Unidentified:
                    NavigationService.NavigateAsync("Login");
                    break;
                case RoleType.Seller:
                    NavigationService.NavigateAsync($"Navigation/SellerMode/SellerProfile");
                    break;
                case RoleType.Buyer:
                    NavigationService.NavigateAsync("Navigation/BuyerMode/BuyerProfile?title=Hello, I'm a buyer");
                    break;
            }
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>("Navigation");
            Container.RegisterTypeForNavigation<SellerMasterPage>("SellerMode");
            Container.RegisterTypeForNavigation<SellerProfile>();
            Container.RegisterTypeForNavigation<BuyerMasterPage>("BuyerMode");
            Container.RegisterTypeForNavigation<BuyerProfile>();
            Container.RegisterTypeForNavigation<RegistrationWizard>();
            Container.RegisterTypeForNavigation<RegistrationName>();
            Container.RegisterTypeForNavigation<Login>();
            Container.RegisterTypeForNavigation<RegistrationEmail>();
            Container.RegisterTypeForNavigation<RegistrationPassword>();
            Container.RegisterTypeForNavigation<AddProduct>();
            Container.RegisterTypeForNavigation<PlaceOrder>();
            Container.RegisterTypeForNavigation<EditProfile>();
        }
    }
}
