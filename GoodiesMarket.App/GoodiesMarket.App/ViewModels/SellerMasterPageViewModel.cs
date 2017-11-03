using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using System.Collections.Generic;
using System.Linq;

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

        private MenuItem selectedItem;
        public MenuItem SelectedItem
        {
            get { return selectedItem; }
            set
            {
                SetProperty(ref selectedItem, value);
                ItemSelected();
            }
        }
        #endregion

        public List<MenuItem> Menu { get; set; }

        public SellerMasterPageViewModel()
        {
            Model = new SellerMasterPageModel
            {
                Username = "Guillermo Herrera",
                Reach = "Alcance: 50 m.",
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

            SelectedItem = Menu.FirstOrDefault();
        }

        private void ItemSelected()
        {
            System.Diagnostics.Debug.WriteLine(selectedItem.Title);
        }
    }
}
