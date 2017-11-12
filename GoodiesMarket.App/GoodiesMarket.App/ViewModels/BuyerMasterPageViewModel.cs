using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using Prism.Commands;
using System.Collections.Generic;

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

        public List<MenuItem> Menu { get; set; }

        public BuyerMasterPageViewModel()
        {
            Model.Name = "Guillermo Herrera";
            Model.Email = "guillermo.herrera@gmail.com";
            Model.PictureUrl = "ic_profile.png";

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
        }

        private void Navigate(MenuItem item)
        {
            System.Diagnostics.Debug.WriteLine(item.Title);
        }
    }
}
