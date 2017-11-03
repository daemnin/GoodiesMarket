namespace GoodiesMarket.App.ViewModels.Abstracts
{
    public class FormViewModelBase : ViewModelBase
    {
        private bool isValid;
        public bool IsValid
        {
            get { return isValid; }
            set { SetProperty(ref isValid, value); }
        }

        public virtual bool Validate()
        {
            return true;
        }

        public bool IsValidModel()
        {
            IsValid = Validate();

            return IsValid;
        }
    }
}
