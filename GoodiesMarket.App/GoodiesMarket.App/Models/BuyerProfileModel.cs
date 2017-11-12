using Prism.Mvvm;

namespace GoodiesMarket.App.Models
{
    public class BuyerProfileModel : BindableBase
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        private string pictureUrl;
        public string PictureUrl
        {
            get { return pictureUrl; }
            set { SetProperty(ref pictureUrl, value); }
        }
    }
}
