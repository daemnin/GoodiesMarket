using Prism.Mvvm;

namespace GoodiesMarket.App.Models
{
    public class SellerMasterPageModel : BindableBase
    {
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
    }
}
