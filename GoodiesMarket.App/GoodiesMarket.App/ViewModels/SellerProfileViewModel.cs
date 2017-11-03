using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using Newtonsoft.Json;
using Prism.Navigation;

namespace GoodiesMarket.App.ViewModels
{
    public class SellerProfileViewModel : ViewModelBase
    {
        private SellerProfileModel model;
        public SellerProfileModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public SellerProfileViewModel()
        {

        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("model"))
            {
                var json = (string)parameters["model"];
                Model = JsonConvert.DeserializeObject<SellerProfileModel>(json);
                Name = model.Name;
            }
        }
    }
}