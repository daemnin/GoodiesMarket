using GoodiesMarket.App.Models;
using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;

namespace GoodiesMarket.App.ViewModels
{
    public class RegistrationEmailViewModel : ViewModelBase
    {
        public ICommand NextCommand { get; private set; }
        private INavigationService navigationService;

        private RegistrationModel model;
        public RegistrationModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }

        public RegistrationEmailViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Model = new RegistrationModel();
            NextCommand = new DelegateCommand(NextView);
        }

        private async void NextView()
        {
            var navigationParameters = new NavigationParameters
            {
                { "model", model }
            };
            await navigationService.NavigateAsync("RegistrationPassword", navigationParameters);

        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("model"))
            {
                Model = (RegistrationModel)parameters["model"];
                System.Diagnostics.Debug.WriteLine(model.IsSeller);
                System.Diagnostics.Debug.WriteLine(model.UserName);
            }
        }
    }
}
