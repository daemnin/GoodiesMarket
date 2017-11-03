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
            var model = new RegistrationModel
            {
                IsSeller = false
            };
            var navigationParameters = new NavigationParameters
            {
                { "model", model }
            };
            await navigationService.NavigateAsync("RegistrationUserName", navigationParameters);

        }

        private async void SellerSelected()
        {
            var model = new RegistrationModel
            {
                IsSeller = true
            };
            var navigationParameters = new NavigationParameters
            {
                { "model", model }
            };
            await navigationService.NavigateAsync("RegistrationUserName", navigationParameters);
        }
    }
}
