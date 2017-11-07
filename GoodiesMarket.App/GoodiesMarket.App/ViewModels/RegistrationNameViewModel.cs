using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using Prism.Commands;
using Prism.Navigation;

namespace GoodiesMarket.App.ViewModels
{
    public class RegistrationNameViewModel : FormViewModelBase
    {
        private INavigationService navigationService;

        public DelegateCommand NextCommand { get; private set; }

        private RegistrationModel model;
        public RegistrationModel Model
        {
            get
            {
                NextCommand.RaiseCanExecuteChanged();
                return model;
            }
            set { SetProperty(ref model, value); }
        }

        public RegistrationNameViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            NextCommand = new DelegateCommand(Next, IsValidModel);
        }

        private async void Next()
        {
            var navigationParameters = new NavigationParameters
            {
                { "model", model }
            };
            await navigationService.NavigateAsync("RegistrationEmail", navigationParameters);
        }

        public override bool Validate()
        {
            return !string.IsNullOrEmpty(model?.Name);
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
