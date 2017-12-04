using System;
using GoodiesMarket.App.Components;
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

        private bool isEditable;
        public bool IsEditable
        {
            get { return isEditable; }
            set { SetProperty(ref isEditable, value); }
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
            if (status == StatusType.Cancelled)
            {
                var delete = await pageDialogService.DisplayAlertAsync("¿Está seguro?",
                                                                       "¿Está seguro de que cancelar esta orden?",
                                                                       "Sí", "No");
                if (!delete) return;
            }

            var proxy = new OrderProxy();

            var result = await proxy.ChangeStatus(Model.Id, status);

            if (result.Succeeded)
            {
                var prev = Model.StatusId;

                if ((prev != StatusType.Cancelled || prev != StatusType.Completed) && status == StatusType.InProgress)
                {
                    var locationResult = await proxy.GetDeliveryLocation(Model.Id);

                    if (locationResult.Succeeded)
                    {
                        var jtoken = locationResult.Response.Value<JToken>("response");
                        var latitude = jtoken.Value<double>("Latitude");
                        var longitude = jtoken.Value<double>("Longitude");
                        DeviceHelper.OpenDeviceMap(latitude, longitude);
                    }
                    else
                    {
                        await pageDialogService.DisplayAlertAsync("Hubo un error", result.Response.ToString(), "Ok");
                    }
                }
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
                IsEditable = Model.StatusId != StatusType.Cancelled;
            }
        }
    }
}
