using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GoodiesMarket.App.Models
{
    public class OrderModel : BindableBase
    {
        private ObservableCollection<Order> orders;
        public ObservableCollection<Order> Orders
        {
            get { return orders; }
            set { SetProperty(ref orders, value); }
        }
    }
}
