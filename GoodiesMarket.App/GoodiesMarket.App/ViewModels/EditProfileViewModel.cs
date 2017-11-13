using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using GoodiesMarket.Components.Proxies;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace GoodiesMarket.App.ViewModels
{
    public class EditProfileViewModel : FormViewModelBase
    {
        private INavigationService navigationService;
        private IPageDialogService pageDialogService;
        private string backupModel;

        private SellerProfileModel model;
        public SellerProfileModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }

        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        public EditProfileViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;

            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private async void Save()
        {
            var proxy = new AccountProxy();

            var response = await proxy.UpdateProfile(model.Motto, model.Range);

            if (response.Succeeded)
            {
                var navParams = new NavigationParameters
                {
                    { "new_model", model }
                };
                await navigationService.GoBackAsync(navParams);
            }
            else
            {
                await pageDialogService.DisplayAlertAsync("Error", response.Response.ToString(), "Ok");
            }
        }

        private async void Cancel()
        {
            var cancel = await pageDialogService.DisplayAlertAsync("¿Está seguro?",
                                                                   "¿Está seguro de que quiere cancelar?",
                                                                   "Sí", "No");
            if (cancel)
            {
                await navigationService.GoBackAsync();
            }
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("model"))
            {
                backupModel = (string)parameters["model"];
                Model = JsonConvert.DeserializeObject<SellerProfileModel>(backupModel);
            }
        }
    }
}
