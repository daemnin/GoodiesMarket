using GoodiesMarket.App.Models;
using System.Collections.Generic;
using System.Linq;

namespace GoodiesMarket.App.ViewModels
{
    public class SellerMasterPageViewModel : ViewModelBase
    {
        #region Properties
        private string username;
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        private string reach;
        public string Reach
        {
            get { return reach; }
            set { SetProperty(ref reach, value); }
        }

        private string profilePicture;
        public string ProfilePicture
        {
            get { return profilePicture; }
            set { SetProperty(ref profilePicture, value); }
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
            Username = "Guillermo Herrera";
            Reach = "Alcance: 50 m.";
            ProfilePicture = "ic_profile.png";

            Menu = new List<MenuItem>
            {
                new MenuItem{ Icon = "ic_profile.png", Text = "Perfil", NavigationUrl = "" },
                new MenuItem{ Icon = "ic_orders.png", Text = "Ordenes", NavigationUrl = "" },
                new MenuItem{ Icon = "ic_edit_reach.png", Text = "Alcance", NavigationUrl = "" },
                new MenuItem{ Icon = "ic_feedback.png", Text = "Ver opiniones", NavigationUrl = "" },
                new MenuItem{ Icon = "ic_shutdown.png", Text = "Salir", NavigationUrl = "" }
            };

            SelectedItem = Menu.FirstOrDefault();
        }

        private void ItemSelected()
        {
            System.Diagnostics.Debug.WriteLine(selectedItem.Text);
        }
    }
}
