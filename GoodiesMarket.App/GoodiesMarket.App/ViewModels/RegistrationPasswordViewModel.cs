using GoodiesMarket.App.Models;
using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;

namespace GoodiesMarket.App.ViewModels
{
    public class RegistrationPasswordViewModel : ViewModelBase
    {
        public ICommand NextCommand { get; private set; }
        private INavigationService navigationService;

        private RegistrationModel model;
        public RegistrationModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }

        public RegistrationPasswordViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Model = new RegistrationModel();
            NextCommand = new DelegateCommand(Next);
        }

        private async void Next()
        {
            var navigationParameters = new NavigationParameters
            {
                { "model", model }
            };

            System.Diagnostics.Debug.WriteLine("--- Final ---");
            System.Diagnostics.Debug.WriteLine(model.IsSeller);
            System.Diagnostics.Debug.WriteLine(model.UserName);
            System.Diagnostics.Debug.WriteLine(model.Email);
            System.Diagnostics.Debug.WriteLine(model.Password);
            System.Diagnostics.Debug.WriteLine(model.PasswordConfirmation);
            // await navigationService.NavigateAsync("", navigationParameters);
        }


        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("model"))
            {
                Model = (RegistrationModel)parameters["model"];
                System.Diagnostics.Debug.WriteLine(model.IsSeller);
                System.Diagnostics.Debug.WriteLine(model.UserName);
                System.Diagnostics.Debug.WriteLine(model.Email);
            }
        }
    }
}
