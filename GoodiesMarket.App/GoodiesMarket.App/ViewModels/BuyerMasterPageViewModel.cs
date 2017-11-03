using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using System.Collections.Generic;
using System.Linq;

namespace GoodiesMarket.App.ViewModels
{
    public class BuyerMasterPageViewModel : ViewModelBase
    {
        #region Properties
        private string username;
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
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

        public BuyerMasterPageViewModel()
        {
            Username = "Guillermo Herrera";
            Email = "guillermo.herrera@gmail.com";
            ProfilePicture = "ic_profile.png";

            Menu = new List<MenuItem>
            {
                new MenuItem{ Icon = "ic_profile.png", Title = "Perfil", NavigationUrl = "" },
                new MenuItem{ Icon = "ic_orders.png", Title = "Ordenes", NavigationUrl = "" },
                new MenuItem{ Icon = "ic_search.png", Title = "Buscar", NavigationUrl = "" },
                new MenuItem{ Icon = "ic_favorites.png", Title = "Ver favoritos", NavigationUrl = "" },
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
