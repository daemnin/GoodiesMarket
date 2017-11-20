using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using GoodiesMarket.Components.Models;
using GoodiesMarket.Components.Proxies;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace GoodiesMarket.App.ViewModels
{
    public class SellerOrderDetailsViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private IPageDialogService pageDialogService;

        public DelegateCommand InProcessCommand { get; private set; }
        public DelegateCommand DelayedCommand { get; private set; }
        public DelegateCommand CancelledCommand { get; private set; }

        private OrderDetailModel model;
        public OrderDetailModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }

        public SellerOrderDetailsViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;

            InProcessCommand = new DelegateCommand(() => ChangeStatus(StatusType.InProgress));
            DelayedCommand = new DelegateCommand(() => ChangeStatus(StatusType.Delayed));
            CancelledCommand = new DelegateCommand(() => ChangeStatus(StatusType.Cancelled));
        }

        private async void ChangeStatus(StatusType status)
        {
            var proxy = new OrderProxy();

            var result = await proxy.ChangeStatus(Model.Id, status);

            if (result.Succeeded)
            {
                var navParams = new NavigationParameters
                {
                    { "update_entry", new { Id = Model.Id, Status = status } }
                };
                await navigationService.GoBackAsync(navParams);
            }
            else
            {
                await pageDialogService.DisplayAlertAsync("Hubo un error", result.Response.ToString(), "Ok");
            }
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("id"))
            {
                var orderId = long.Parse(parameters["id"].ToString());

                Load(orderId);
            }
        }

        private async void Load(long orderId)
        {
            var proxy = new OrderProxy();

            var result = await proxy.Get(orderId);

            if (result.Succeeded)
            {
                Model = result.Response.Value<JToken>("response").ToObject<OrderDetailModel>();
            }
        }
    }
}
