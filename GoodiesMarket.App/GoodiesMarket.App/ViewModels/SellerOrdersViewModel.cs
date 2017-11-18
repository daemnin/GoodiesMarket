using GoodiesMarket.App.Models;
using Prism.Commands;
using System.Collections.Generic;
using Prism.Navigation;
using Prism.Services;
using GoodiesMarket.App.ViewModels.Abstracts;
using GoodiesMarket.Components.Proxies;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using Xamarin.Forms.Internals;

namespace GoodiesMarket.App.ViewModels
{
    public class SellerOrdersViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private IPageDialogService pageDialogService;

        private OrderModel model;
        public OrderModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }

        public DelegateCommand<Order> ViewDetailsCommand { get; private set; }

        public SellerOrdersViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;

            ViewDetailsCommand = new DelegateCommand<Order>(ViewDetails);
        }

        private async void ViewDetails(Order order)
        {
            await navigationService.NavigateAsync($"SellerOrderDetails?id={order.Id}", useModalNavigation: true);
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            Load();
        }

        private async void Load()
        {
            var proxy = new OrderProxy();

            var result = await proxy.Get();

            if (result.Succeeded)
            {
                if (Model == null)
                {
                    Model = new OrderModel();
                    Model.Orders = new ObservableCollection<Order>();
                }
                Model.Orders.Clear();
                result.Response.Value<JToken>("response")
                               .ToObject<IList<Order>>()
                               .ForEach(o => Model.Orders.Add(o));
            }
            else
            {
                await pageDialogService.DisplayAlertAsync("Hubo un error", result.Response.ToString(), "Ok");
            }
        }
    }
}
