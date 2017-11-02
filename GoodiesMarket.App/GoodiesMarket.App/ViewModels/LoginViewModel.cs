using GoodiesMarket.App.Models;
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
        public ICommand LoginCommand { get; private set; }

        public LoginViewModel(INavigationService navigationService)
        {
            Model = new LoginModel();
            LoginCommand = new DelegateCommand(Login);
            RegistrationCommand = new DelegateCommand(async () => await navigationService.NavigateAsync("RegistrationWizard"));
        }

        private async void Login()
        {
            System.Diagnostics.Debug.WriteLine(Model.Email);
            System.Diagnostics.Debug.WriteLine(Model.Password);
        }
    }
}
