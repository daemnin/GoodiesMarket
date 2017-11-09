using Prism.Mvvm;

namespace GoodiesMarket.App.Models
{
    public class SellerMasterPageModel : BindableBase
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string range;
        public string Range
        {
            get { return range; }
            set { SetProperty(ref range, value); }
        }

        private string profilePicture;
        public string ProfilePicture
        {
            get { return profilePicture; }
            set { SetProperty(ref profilePicture, value); }
        }
    }
}
