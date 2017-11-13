using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Linq;

namespace GoodiesMarket.App.ViewModels
{
    public class BuyerMasterPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private IPageDialogService pageDialogService;

        public DelegateCommand<MenuItem> SelectCommand { get; private set; }

        #region Properties
        private BuyerProfileModel model;
        public BuyerProfileModel Model
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

        public List<MenuItem> Menu { get; set; }

        public BuyerMasterPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;

            Menu = new List<MenuItem>
            {
                new MenuItem{ Icon = "ic_profile.png", Title = "Perfil", NavigationUrl = "BuyerProfile" },
                new MenuItem{ Icon = "ic_orders.png", Title = "Ordenes", NavigationUrl = "" },
                new MenuItem{ Icon = "ic_search.png", Title = "Buscar", NavigationUrl = "" },
                new MenuItem{ Icon = "ic_favorites.png", Title = "Ver favoritos", NavigationUrl = "" },
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
            if (parameters.ContainsKey("model"))
            {
                Model = (BuyerProfileModel)parameters["model"];
            }
        }
    }
}
