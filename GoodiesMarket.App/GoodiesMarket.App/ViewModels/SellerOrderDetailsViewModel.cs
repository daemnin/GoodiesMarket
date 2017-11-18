using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using Prism.Navigation;
using GoodiesMarket.Components.Proxies;
using System.Diagnostics;
using Prism.Services;
using Prism.Commands;
using GoodiesMarket.Components.Models;
using Newtonsoft.Json.Linq;

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

            //Model = new OrderDetailModel
            //{
            //    Buyer = "Daniel Siordia",
            //    Note = "Si tuvieras extra caca, estaria bien chingon, hechale un peso de caca extra",
            //    CreatedOn = DateTime.Now,
            //    Id = 420,
            //    Total = 30f,
            //    Products = new List<OrderDetail>
            //    {
            //        new OrderDetail
            //        {
            //            Name = "Galletas de caca",
            //            Quantity = 10
            //        },
            //        new OrderDetail
            //        {
            //            Name = "Galletas de avena",
            //            Quantity = 5
            //        },
            //    }

            //};
        }

        private async void ChangeStatus(StatusType status)
        {
            var proxy = new OrderProxy();

            var result = await proxy.ChangeStatus(Model.Id, status);

            if (result.Succeeded)
            {
                Debug.WriteLine(result);
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
