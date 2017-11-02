using Prism.Mvvm;

namespace GoodiesMarket.App.Models
{
    public class LoginModel : BindableBase
    {
        private string email;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }
    }
}
