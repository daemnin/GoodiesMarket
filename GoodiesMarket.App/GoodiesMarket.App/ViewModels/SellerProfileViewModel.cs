using System;
using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using GoodiesMarket.Components.Proxies;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using GoodiesMarket.App.Components;

namespace GoodiesMarket.App.ViewModels
{
    public class SellerProfileViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private IPageDialogService pageDialogService;

        public DelegateCommand<ProductModel> EditCommand { get; private set; }
        public DelegateCommand<ProductModel> DeleteCommand { get; private set; }
        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand EditProfileCommand { get; private set; }
        public DelegateCommand UpdateLocationCommand { get; private set; }

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
            EditProfileCommand = new DelegateCommand(EditProfile);
            UpdateLocationCommand = new DelegateCommand(UpdateLocation);
        }

        private async void UpdateLocation()
        {
            var proxy = new AccountProxy();

            var location = await DeviceHelper.GetLocation();

            var result = await proxy.UpdateProfile(latitude: location?.Latitude, longitude: location?.Longitude);

            if (!result.Succeeded)
            {
                await pageDialogService.DisplayAlertAsync("Error", result.Response.ToString(), "Ok");
            }
        }

        private async void EditProfile()
        {
            var navParams = new NavigationParameters
            {
                { "model", JsonConvert.SerializeObject(model) }
            };
            await navigationService.NavigateAsync("EditProfile", navParams, useModalNavigation: true);
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

        private async void GetProfile()
        {
            var proxy = new AccountProxy();
            var response = await proxy.GetProfile();

            if (response.Succeeded)
            {
                Model = response.Response.Value<JToken>("response").ToObject<SellerProfileModel>();
                Model.StarUrl = "ic_rating_star";
            }
            else
            {
                await pageDialogService.DisplayAlertAsync("Hubo un error", "Se generó un problema, inicie sesión nuevamente.", "Ok");
                await navigationService.NavigateAsync("/Login?SignOut");
            }
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("model") && model == null)
            {
                Model = (SellerProfileModel)parameters["model"];
            }
            else
            {
                GetProfile();
            }

            if (parameters.ContainsKey("new_product"))
            {
                Model.Products.Add((ProductModel)parameters["new_product"]);
            }

            if (parameters.ContainsKey("new_model"))
            {
                Model = (SellerProfileModel)parameters["new_model"];
            }
        }
    }
}