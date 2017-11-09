using GoodiesMarket.App.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;

namespace GoodiesMarket.App.ViewModels
{
    public class PlaceOrderViewModel : BindableBase
    {
        private PlaceOrderModel model;
        public PlaceOrderModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }
        public DelegateCommand<OrderProduct> RemoveCommand { get; private set; }
        public DelegateCommand<OrderProduct> AddCommand { get; private set; }
        public DelegateCommand PlaceOrderCommand { get; private set; }
        public PlaceOrderViewModel()
        {
            RemoveCommand = new DelegateCommand<OrderProduct>(RemoveProduct);
            AddCommand = new DelegateCommand<OrderProduct>(AddProduct);
            PlaceOrderCommand = new DelegateCommand(PlaceOrder);

            Model = new PlaceOrderModel
            {
                SellerName = "Don Cirilo",
                PurchaseRestriction = "No le vendo a muerde almohadas",
                Total = 0,
                Products = new List<OrderProduct>
                {
                    new OrderProduct
                    {
                        Name = "Coca-cola 355ml",
                        Amount = 0,
                        PictureUrl = "ic_camera.png",
                        Price = 12.50f,
                        Stock = 15
                    },
                    new OrderProduct
                    {
                        Name = "Tonayan 1lt",
                        Amount = 0,
                        PictureUrl = "ic_camera.png",
                        Price = 5.50f,
                        Stock = 10
                    },
                    new OrderProduct
                    {
                        Name = "Tonayan 1lt",
                        Amount = 0,
                        PictureUrl = "ic_camera.png",
                        Price = 5.50f,
                        Stock = 10
                    },
                    new OrderProduct
                    {
                        Name = "Tonayan 1lt",
                        Amount = 0,
                        PictureUrl = "ic_camera.png",
                        Price = 5.50f,
                        Stock = 10
                    }

                }
            };

        }

        private void PlaceOrder()
        {

        }

        private void AddProduct(OrderProduct product)
        {
            if (product.Amount < product.Stock)
            {
                product.Amount++;
            }
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            Model.Total = Model.Products.Sum(p => p.Amount * p.Price);
        }

        private void RemoveProduct(OrderProduct product)
        {
            if (product.Amount >= 1)
            {
                product.Amount--;
            }
            UpdateTotal();
        }
    }
}
