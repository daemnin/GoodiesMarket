using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using GoodiesMarket.Components.Proxies;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace GoodiesMarket.App.ViewModels
{
    public class SellerProfileViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private IPageDialogService pageDialogService;

        public DelegateCommand<ProductModel> EditCommand { get; private set; }
        public DelegateCommand<ProductModel> DeleteCommand { get; private set; }
        public DelegateCommand AddCommand { get; private set; }

        private SellerProfileModel model;
        public SellerProfileModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }

        public SellerProfileViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;
            AddCommand = new DelegateCommand(AddProduct);
            EditCommand = new DelegateCommand<ProductModel>(EditProduct);
            DeleteCommand = new DelegateCommand<ProductModel>(DeleteProduct);
        }

        private async void AddProduct()
        {
            await navigationService.NavigateAsync("AddProduct", useModalNavigation: true);
        }

        private async void DeleteProduct(ProductModel product)
        {
            var delete = await pageDialogService.DisplayAlertAsync("¿Está seguro?",
                                                                   "¿Está seguro de que quiere eliminar este producto?",
                                                                   "Sí", "No");
            if (delete)
            {
                var proxy = new ProductProxy();

                var response = await proxy.Delete(product.Id);

                if (response.Succeeded)
                {
                    Model.Products.Remove(product);
                }
                else
                {
                    await pageDialogService.DisplayAlertAsync("Hubo un error", response.Response.ToString(), "Ok");
                }
            }
        }

        private async void EditProduct(ProductModel product)
        {
            var navParams = new NavigationParameters
            {
                { "model", product }
            };
            await navigationService.NavigateAsync("AddProduct", navParams, useModalNavigation: true);
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("model"))
            {
                Model = (SellerProfileModel)parameters["model"];
            }

            if (parameters.ContainsKey("new_product"))
            {
                Model.Products.Add((ProductModel)parameters["new_product"]);
            }
        }
    }
}