using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using GoodiesMarket.Components.Helpers;
using GoodiesMarket.Components.Models;
using GoodiesMarket.Components.Proxies;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;

namespace GoodiesMarket.App.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private LoginModel model;
        public LoginModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }
        public ICommand RegistrationCommand { get; private set; }
        public ICommand SignInCommand { get; private set; }

        private INavigationService navigationService;

        public LoginViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Model = new LoginModel();
            SignInCommand = new DelegateCommand(SignIn);
            RegistrationCommand = new DelegateCommand(Register);

            Model.Email = "daniel@abc.com";
            Model.Password = "password123";
        }

        private async void Register()
        {
            await navigationService.NavigateAsync("RegistrationWizard");
        }

        private async void SignIn()
        {
            var proxy = new AccountProxy();

            var signInResponse = await proxy.SignIn(model.Email, model.Password);

            if (signInResponse.Succeeded)
            {
                Credentials.Instance.SignIn(signInResponse.Response);

                var roleResponse = await proxy.GetRole();

                var role = (RoleType)roleResponse.Response.Value<int>("role");

                var profileResponse = await proxy.GetProfile();
                var navParams = new NavigationParameters();

                switch (role)
                {
                    case RoleType.Buyer:
                        var buyerModel = profileResponse.Response.Value<JToken>("response").ToObject<BuyerProfileModel>();
                        navParams.Add("model", buyerModel);
                        await navigationService.NavigateAsync("/Navigation/BuyerMode/BuyerProfile", navParams);
                        break;
                    case RoleType.Seller:
                        var sellerModel = profileResponse.Response.Value<JToken>("response").ToObject<SellerProfileModel>();
                        sellerModel.StarUrl = "ic_rating_star";
                        navParams.Add("model", sellerModel);
                        await navigationService.NavigateAsync("/Navigation/SellerMode/SellerProfile", navParams);
                        break;
                }
            }
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("SignOut"))
            {
                Credentials.Instance.SignOut();
            }
        }
    }
}
