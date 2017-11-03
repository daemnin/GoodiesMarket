using GoodiesMarket.App.Models;
using Prism.Commands;
using Prism.Navigation;

namespace GoodiesMarket.App.ViewModels
{
    public class RegistrationUserNameViewModel : ViewModelBase
    {
        private INavigationService navigationService;

        public DelegateCommand NextCommand { get; private set; }

        private RegistrationModel model;
        public RegistrationModel Model
        {
            get { NextCommand.RaiseCanExecuteChanged(); return model; }
            set { SetProperty(ref model, value); }
        }

        private bool isValid;
        public bool IsValid
        {
            get { return isValid; }
            set { SetProperty(ref isValid, value); }
        }

        public RegistrationUserNameViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Model = new RegistrationModel();
            NextCommand = new DelegateCommand(Next, IsValidModel);
        }

        public bool IsValidModel()
        {
            IsValid = !string.IsNullOrEmpty(model.UserName);
            return IsValid;
        }

        private async void Next()
        {
            var navigationParameters = new NavigationParameters
            {
                { "model", model }
            };
            await navigationService.NavigateAsync("RegistrationEmail", navigationParameters);
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("model"))
            {
                Model = (RegistrationModel)parameters["model"];
                System.Diagnostics.Debug.WriteLine(model.IsSeller);
            }
        }
    }
}
