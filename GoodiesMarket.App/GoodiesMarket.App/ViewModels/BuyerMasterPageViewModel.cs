using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;

namespace GoodiesMarket.App.ViewModels
{
    public class BuyerMasterPageViewModel : ViewModelBase
    {
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

        public BuyerMasterPageViewModel()
        {
            Menu = new List<MenuItem>
            {
                new MenuItem{ Icon = "ic_profile.png", Title = "Perfil", NavigationUrl = "" },
                new MenuItem{ Icon = "ic_orders.png", Title = "Ordenes", NavigationUrl = "" },
                new MenuItem{ Icon = "ic_search.png", Title = "Buscar", NavigationUrl = "" },
                new MenuItem{ Icon = "ic_favorites.png", Title = "Ver favoritos", NavigationUrl = "" },
                new MenuItem{ Icon = "ic_feedback.png", Title = "Ver opiniones", NavigationUrl = "" },
                new MenuItem{ Icon = "ic_shutdown.png", Title = "Salir", NavigationUrl = "" }
            };

            SelectCommand = new DelegateCommand<MenuItem>(Navigate);
            Title = Menu.FirstOrDefault().Title;
        }

        private void Navigate(MenuItem item)
        {
            Title = item.Title;
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
