using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using GoodiesMarket.Components.Proxies;
using Prism.Commands;
using Prism.Navigation;

namespace GoodiesMarket.App.ViewModels
{
    public class RegistrationPasswordViewModel : FormViewModelBase
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

        public RegistrationPasswordViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Model = new RegistrationModel();
            NextCommand = new DelegateCommand(Next, IsValidModel);
        }

        private async void Next()
        {
            var proxy = new AccountProxy();

            var result = await proxy.Register(model.Name, model.Email, model.Password, model.IsSeller);
        }

        public override bool Validate()
        {
            if (string.IsNullOrEmpty(model?.Password)) return false;
            if (string.IsNullOrEmpty(model.PasswordConfirmation)) return false;

            return model.Password == model.PasswordConfirmation;
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("model"))
            {
                Model = (RegistrationModel)parameters["model"];
                System.Diagnostics.Debug.WriteLine(model.IsSeller);
                System.Diagnostics.Debug.WriteLine(model.Name);
                System.Diagnostics.Debug.WriteLine(model.Email);
            }
        }
    }
}
