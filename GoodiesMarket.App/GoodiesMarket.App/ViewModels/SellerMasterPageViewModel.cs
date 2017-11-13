using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Linq;

namespace GoodiesMarket.App.ViewModels
{
    public class SellerMasterPageViewModel : ViewModelBase
    {
        #region Properties
        private SellerProfileModel model;
        public SellerProfileModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }
        #endregion

        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private IPageDialogService pageDialogService;
        private INavigationService navigationService;

        public List<MenuItem> Menu { get; set; }

        public DelegateCommand<MenuItem> SelectCommand { get; private set; }

        public SellerMasterPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this.pageDialogService = pageDialogService;
            this.navigationService = navigationService;
            Menu = new List<MenuItem>
            {
                new MenuItem{ Icon = "ic_profile.png", Title = "Perfil", NavigationUrl = "SellerProfile" },
                new MenuItem{ Icon = "ic_orders.png", Title = "Ordenes", NavigationUrl = "BuyerProfile" },
                new MenuItem{ Icon = "ic_feedback.png", Title = "Ver opiniones", NavigationUrl = "" },
                new MenuItem{ Icon = "ic_shutdown.png", Title = "Salir", NavigationUrl = "/Login?SignOut" }
            };

            SelectCommand = new DelegateCommand<MenuItem>(Navigate);
            Title = Menu.FirstOrDefault().Title;
        }

        private async void Navigate(MenuItem item)
        {
            if (item.Title.Equals("Salir"))
            {
                var confirm = await pageDialogService.DisplayAlertAsync("Confirmación", "Está apunto de cerrar sesión.", "Ok", "Cancelar");
                if (!confirm) return;
            }
            Title = item.Title;
            await navigationService.NavigateAsync(item.NavigationUrl);
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("something"))
            {

            }
            if (parameters.ContainsKey("model"))
            {
                Model = (SellerProfileModel)parameters["model"];
            }
        }
    }
}