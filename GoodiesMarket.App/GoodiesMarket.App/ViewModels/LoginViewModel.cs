using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using GoodiesMarket.Components.Helpers;
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

            Model.Email = "guillermo@abc.com";
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

                var profileResponse = await proxy.GetProfile();

                var response = profileResponse.Response.Value<JToken>("response").ToObject<BuyerProfileModel>();

                System.Diagnostics.Debug.WriteLine(response);
            }
        }
    }
}
