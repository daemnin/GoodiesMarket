using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using Prism.Navigation;

namespace GoodiesMarket.App.ViewModels
{
    public class BuyerProfileViewModel : ViewModelBase
    {
        private BuyerProfileModel model;
        public BuyerProfileModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }

        public BuyerProfileViewModel()
        {

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