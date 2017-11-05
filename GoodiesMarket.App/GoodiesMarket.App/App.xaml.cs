using GoodiesMarket.App.Models;
using GoodiesMarket.App.Views;
using Newtonsoft.Json;
using Prism.Navigation;
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
            var navigationParameters = new NavigationParameters();
            var m = new SellerProfileModel
            {
                Name = "Don Cirilo",
                PictureUrl = "ic_profile.png",
                Motto = "Si se hace!",
                Score = 4.2f,
                StarUrl = "ic_rating_star.png",
                Products = new System.Collections.Generic.List<ProductModel> {

                    new ProductModel
                    {
                        Name = "Sabritones",
                        Inventory = 420,
                        PictureUrl = "ic_profile.png"
                    },
                    new ProductModel
                    {
                        Name = "Taquitos",
                        Inventory = 68,
                        PictureUrl = "ic_profile.png"
                    },
                    new ProductModel
                    {
                        Name = "Gabbo pendejo",
                        Inventory = 1,
                        PictureUrl = "ic_profile.png"
                    }
                }
            };

            string json = JsonConvert.SerializeObject(m);
            navigationParameters.Add("model", json);

            var userType = UserType.Seller;
            switch (userType)
            {
                case UserType.Unregistered:
                    NavigationService.NavigateAsync("Login");
                    break;
                case UserType.Seller:

                    NavigationService.NavigateAsync($"NavigationPage/SellerMasterPage/SellerProfile", navigationParameters);
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
            Container.RegisterTypeForNavigation<RegistrationUserName>();
            Container.RegisterTypeForNavigation<Login>();
            Container.RegisterTypeForNavigation<RegistrationEmail>();
            Container.RegisterTypeForNavigation<RegistrationPassword>();
            Container.RegisterTypeForNavigation<AddProduct>();
        }
    }
}
