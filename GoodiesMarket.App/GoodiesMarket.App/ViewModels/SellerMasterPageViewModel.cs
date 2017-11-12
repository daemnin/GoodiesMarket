using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using Prism.Commands;
using Prism.Navigation;
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

        public List<MenuItem> Menu { get; set; }

        public DelegateCommand<MenuItem> SelectCommand { get; private set; }

        public SellerMasterPageViewModel()
        {
            Menu = new List<MenuItem>
            {
                new MenuItem{ Icon = "ic_profile.png", Title = "Perfil", NavigationUrl = "" },
                new MenuItem{ Icon = "ic_orders.png", Title = "Ordenes", NavigationUrl = "" },
                new MenuItem{ Icon = "ic_edit_reach.png", Title = "Alcance", NavigationUrl = "" },
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
                Model = (SellerProfileModel)parameters["model"];
            }
        }
    }
}
