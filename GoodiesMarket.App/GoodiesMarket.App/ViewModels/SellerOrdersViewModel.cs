using GoodiesMarket.App.Models;
using GoodiesMarket.App.ViewModels.Abstracts;
using GoodiesMarket.Components.Proxies;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
            Model = new OrderModel
            {
                Orders = new ObservableCollection<Order>()
            };
        }

        private async void ViewDetails(Order order)
        {
            await navigationService.NavigateAsync($"SellerOrderDetails?id={order.Id}", useModalNavigation: true);
        }

        public async override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Load"))
            {
                await Load();
            }
            if (parameters.ContainsKey("update_entry"))
            {
                var entry = (dynamic)parameters["update_entry"];
                Model.Orders.FirstOrDefault(o => o.Id.Equals(entry.Id)).Status = entry.Status;
            }
        }

        private async Task Load()
        {
            var proxy = new OrderProxy();

            var result = await proxy.Get();

            if (result.Succeeded)
            {
                Model.Orders.Clear();
                var orders = result.Response.Value<JToken>("response").ToObject<IEnumerable<Order>>();
                orders.ForEach(o => Model.Orders.Add(o));
            }
            else
            {
                await pageDialogService.DisplayAlertAsync("Hubo un error", result.Response.ToString(), "Ok");
            }
        }
    }
}
