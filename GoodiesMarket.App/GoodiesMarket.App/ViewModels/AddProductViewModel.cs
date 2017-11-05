using GoodiesMarket.App.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace GoodiesMarket.App.ViewModels
{
    public class AddProductViewModel : BindableBase
    {
        public DelegateCommand SelectPictureCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        private ProductModel model;
        public ProductModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }

        public AddProductViewModel()
        {
            Model = new ProductModel();
            SelectPictureCommand = new DelegateCommand(SelectPicture);
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private void Cancel()
        {
        }

        private void Save()
        {
        }

        private void SelectPicture()
        {
        }
    }
}
