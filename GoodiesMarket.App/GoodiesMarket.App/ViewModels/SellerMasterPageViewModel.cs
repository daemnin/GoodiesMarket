using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using Prism.Commands;
using System.Collections.Generic;

namespace GoodiesMarket.App.ViewModels
{
    public class SellerMasterPageViewModel : ViewModelBase
    {
        #region Properties
        private SellerMasterPageModel model;
        public SellerMasterPageModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }
        #endregion

        public List<MenuItem> Menu { get; set; }

        public DelegateCommand<MenuItem> SelectCommand { get; private set; }

        public SellerMasterPageViewModel()
        {
            Model = new SellerMasterPageModel
            {
                Name = "Guillermo Herrera",
                Range = "Alcance: 50 m.",
                ProfilePicture = "ic_profile.png"
            };

            Menu = new List<MenuItem>
            {
                new MenuItem{ Icon = "ic_profile.png", Title = "Perfil", NavigationUrl = "" },
                new MenuItem{ Icon = "ic_orders.png", Title = "Ordenes", NavigationUrl = "" },
                new MenuItem{ Icon = "ic_edit_reach.png", Title = "Alcance", NavigationUrl = "" },
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
