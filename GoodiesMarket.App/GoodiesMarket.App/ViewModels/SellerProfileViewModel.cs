using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;

namespace GoodiesMarket.App.ViewModels
{
    public class SellerProfileViewModel : ViewModelBase
    {
        public DelegateCommand<ProductModel> EditCommand { get; private set; }
        public DelegateCommand<ProductModel> DeleteCommand { get; private set; }

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

        private ProductModel selectedItem;
        public ProductModel SelectedItem
        {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem, value); }
        }

        public SellerProfileViewModel()
        {
            EditCommand = new DelegateCommand<ProductModel>(EditProduct);
            DeleteCommand = new DelegateCommand<ProductModel>(DeleteProduct);
        }

        private void DeleteProduct(ProductModel product)
        {
            System.Diagnostics.Debug.WriteLine(product.Name);
        }

        private void EditProduct(ProductModel product)
        {
            System.Diagnostics.Debug.WriteLine(product.Name);
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