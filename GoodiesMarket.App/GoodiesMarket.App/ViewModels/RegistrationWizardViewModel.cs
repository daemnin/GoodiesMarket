using GoodiesMarket.App.Models;
using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;

namespace GoodiesMarket.App.ViewModels
{
    public class RegistrationWizardViewModel : ViewModelBase
    {
        public ICommand SellerCommand { get; private set; }
        public ICommand BuyerCommand { get; private set; }
        private INavigationService navigationService;

        public RegistrationWizardViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            SellerCommand = new DelegateCommand(SellerSelected);
            BuyerCommand = new DelegateCommand(BuyerSelected);
        }

        private async void BuyerSelected()
        {
            var navigationParameters = new NavigationParameters();
            var model = new RegistrationModel
            {
                IsSeller = false
            };
            navigationParameters.Add("model", model);
            await navigationService.NavigateAsync("RegistrationUserName", navigationParameters);

        }

        private async void SellerSelected()
        {
            var navigationParameters = new NavigationParameters();
            var model = new RegistrationModel
            {
                IsSeller = true
            };
            navigationParameters.Add("model", model);
            await navigationService.NavigateAsync("RegistrationUserName", navigationParameters);
        }
    }
}
