using GoodiesMarket.App.Components.Proxies;
using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
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

        public LoginViewModel(INavigationService navigationService)
        {
            Model = new LoginModel();
            SignInCommand = new DelegateCommand(SignIn);
            RegistrationCommand = new DelegateCommand(async () => await navigationService.NavigateAsync("RegistrationWizard"));
        }

        private async void SignIn()
        {
            var proxy = new AccountProxy();

            var response = await proxy.SignIn(model);
            System.Diagnostics.Debug.WriteLine(response.StatusCode);
            System.Diagnostics.Debug.WriteLine(response.Message);
            System.Diagnostics.Debug.WriteLine(response.Response);
        }
    }
}
