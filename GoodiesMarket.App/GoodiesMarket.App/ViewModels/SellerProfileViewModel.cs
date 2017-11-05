using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace GoodiesMarket.App.ViewModels
{
    public class SellerProfileViewModel : ViewModelBase
    {
        public DelegateCommand ModifyCommand { get; private set; }

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

        private IPageDialogService dialogService;

        public SellerProfileViewModel(IPageDialogService dialogService)
        {

            this.dialogService = dialogService;
            ModifyCommand = new DelegateCommand(OpenMenu);

        }

        private async void OpenMenu()
        {
            var action = await dialogService.DisplayActionSheetAsync("ActionSheet: Send to?", "Cancel", null, "Modificar");
            System.Diagnostics.Debug.WriteLine("Action: " + action); // writes the selected button label to the console
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