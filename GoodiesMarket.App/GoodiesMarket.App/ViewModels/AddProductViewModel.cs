using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using GoodiesMarket.Components.Proxies;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Threading.Tasks;

namespace GoodiesMarket.App.ViewModels
{
    public class AddProductViewModel : FormViewModelBase
    {
        private INavigationService navigationService;
        private IPageDialogService pageDialogService;

        private string modelBackup;

        public DelegateCommand SelectPictureCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        private ProductModel model;
        public ProductModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }

        public AddProductViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;
            SelectPictureCommand = new DelegateCommand(SelectPicture);
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private async void Cancel()
        {
            var cancel = await pageDialogService.DisplayAlertAsync("¿Está seguro?",
                                                                   "¿Está seguro de que quiere cancelar?",
                                                                   "Sí", "No");

            if (cancel)
            {
                if (!string.IsNullOrEmpty(modelBackup))
                {
                    var backup = JsonConvert.DeserializeObject<ProductModel>(modelBackup);
                    Model.Description = backup.Description;
                    Model.Name = backup.Name;
                    Model.Price = backup.Price;
                    Model.Stock = backup.Stock;
                    Model.ImageUrl = backup.ImageUrl;
                }

                await navigationService.GoBackAsync();
            }
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(modelBackup))
            {
                await Create();
            }
            else
            {
                await Update();
            }

        }

        private async Task Update()
        {
            var proxy = new ProductProxy();

            var response = await proxy.Update(model.Id, model.Name, model.Description, model.Stock, model.Price);

            if (response.Succeeded)
            {
                await navigationService.GoBackAsync();
            }
            else
            {
                await pageDialogService.DisplayAlertAsync("Hubo un error", response.Response.ToString(), "Ok");
            }
        }

        private async Task Create()
        {
            var proxy = new ProductProxy();

            var response = await proxy.Create(model.Name, model.Description, model.Stock, model.Price);

            if (response.Succeeded)
            {
                model.Id = response.Response.Value<JToken>("response").Value<int>("Id");
                var navParams = new NavigationParameters
                {
                    { "new_product", model }
                };
                await navigationService.GoBackAsync(navParams);
            }
            else
            {
                await pageDialogService.DisplayAlertAsync("Hubo un error", response.Response.ToString(), "Ok");
            }
        }

        private async void SelectPicture()
        {
            await pageDialogService.DisplayAlertAsync("Deshabilitado", "Esta opción aun no está habilitada.", "Ok");
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("model"))
            {
                Model = (ProductModel)parameters["model"];
                modelBackup = JsonConvert.SerializeObject(parameters["model"]);
            }
            else
            {
                Model = new ProductModel();
            }
        }
    }
}