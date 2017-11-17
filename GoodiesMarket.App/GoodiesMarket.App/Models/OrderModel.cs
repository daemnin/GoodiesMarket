using Prism.Mvvm;
using System.Collections.Generic;

namespace GoodiesMarket.App.Models
{
    public class OrderModel : BindableBase
    {
        public List<Order> Orders { get; set; }
    }
}
