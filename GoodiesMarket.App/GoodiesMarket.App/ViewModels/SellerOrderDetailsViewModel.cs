using GoodiesMarket.App.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;

namespace GoodiesMarket.App.ViewModels
{
    public class SellerOrderDetailsViewModel : BindableBase
    {
        private OrderDetailModel model;
        public OrderDetailModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }

        public SellerOrderDetailsViewModel()
        {
            Model = new OrderDetailModel
            {
                Buyer = "Daniel Siordia",
                Note = "Si tuvieras extra caca, estaria bien chingon, hechale un peso de caca extra",
                CreatedOn = DateTime.Now,
                Id = 420,
                Total = 30f,
                Products = new List<OrderDetail>
                {
                    new OrderDetail
                    {
                        Name = "Galletas de caca",
                        Quantity = 10
                    },
                    new OrderDetail
                    {
                        Name = "Galletas de avena",
                        Quantity = 5
                    },
                }

            };
        }
    }
}
