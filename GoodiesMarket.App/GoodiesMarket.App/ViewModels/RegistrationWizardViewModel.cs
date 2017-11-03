using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
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
            SellerCommand = new DelegateCommand(() => Select(isSeller: true));
            BuyerCommand = new DelegateCommand(() => Select());
        }

        private async void Select(bool isSeller = false)
        {
            var model = new RegistrationModel
            {
                IsSeller = isSeller
            };
            var navigationParameters = new NavigationParameters
            {
                { "model", model }
            };
            await navigationService.NavigateAsync("RegistrationUserName", navigationParameters);
        }
    }
}
