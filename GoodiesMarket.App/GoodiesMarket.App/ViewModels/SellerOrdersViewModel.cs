using GoodiesMarket.App.Models;
using Prism.Mvvm;
using System.Collections.Generic;

namespace GoodiesMarket.App.ViewModels
{
    public class SellerOrdersViewModel : BindableBase
    {
        private OrderModel model;
        public OrderModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }

        public SellerOrdersViewModel()
        {
            Model = new OrderModel
            {
                Orders = new List<Order>
                {
                    new Order
                    {
                        Buyer = "Daniel Siordia",
                        Total = 10.50f
                    },
                    new Order
                    {
                        Buyer = "Josue Gonzalez",
                        Total = 120.50f
                    }
                }
            };
        }
    }
}
