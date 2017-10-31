using Prism.Commands;
using Prism.Navigation;

namespace GoodiesMarket.App.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public DelegateCommand RegistrationCommand { get; private set; }
        public DelegateCommand LoginCommand { get; private set; }

        public LoginViewModel(INavigationService navigationService)
        {
            LoginCommand = new DelegateCommand(Login);
            RegistrationCommand = new DelegateCommand(async () => await navigationService.NavigateAsync("RegistrationWizard"));
        }

        private async void Login()
        {
            System.Diagnostics.Debug.WriteLine("Login");
        }
    }
}
