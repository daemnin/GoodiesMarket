using GoodiesMarket.App.ViewModels.Abstracts;
using Prism.Navigation;

namespace GoodiesMarket.App.ViewModels
{
    public class BuyerProfileViewModel : ViewModelBase
    {
        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public BuyerProfileViewModel()
        {

        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("title"))
                Title = (string)parameters["title"];
        }
    }
}
