using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using Prism.Commands;
using Prism.Navigation;

namespace GoodiesMarket.App.ViewModels
{
    public class RegistrationEmailViewModel : FormViewModelBase
    {
        public DelegateCommand NextCommand { get; private set; }
        private INavigationService navigationService;

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

        public RegistrationEmailViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Model = new RegistrationModel();
            NextCommand = new DelegateCommand(Next, IsValidModel);
        }

        private async void Next()
        {
            var navigationParameters = new NavigationParameters
            {
                { "model", model }
            };
            await navigationService.NavigateAsync("RegistrationPassword", navigationParameters);

        }

        public override bool Validate()
        {
            return !string.IsNullOrEmpty(model?.Email);
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("model"))
            {
                Model = (RegistrationModel)parameters["model"];
            }
        }
    }
}
