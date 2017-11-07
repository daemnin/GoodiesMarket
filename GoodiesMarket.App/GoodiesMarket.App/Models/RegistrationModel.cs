using Prism.Mvvm;

namespace GoodiesMarket.App.Models
{
    public class RegistrationModel : BindableBase
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

        private string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        private string passwordConfirmation;
        public string PasswordConfirmation
        {
            get { return passwordConfirmation; }
            set { SetProperty(ref passwordConfirmation, value); }
        }

        private bool isSeller;
        public bool IsSeller
        {
            get { return isSeller; }
            set { SetProperty(ref isSeller, value); }
        }


    }
}
