using GoodiesMarket.Components.Extensions;
using GoodiesMarket.Components.Models;
using GoodiesMarket.Data.Contracts;
using GoodiesMarket.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoodiesMarket.Business.Processes
{
    public class OrderProcess : ProcessBase
    {
        public OrderProcess(IUnitOfWork unitOfWork) : base(unitOfWork) { }


        public Result Create(Guid userId, Guid sellerId, string note, IEnumerable<Tuple<long, int>> products)
        {
            var result = new Result();
            try
            {
                var order = CreateOrder(userId, sellerId, note);
                var total = AssingProductsToOrder(order, products);
                result.Succeeded = UpdateTotal(order, total);
                result.Response = (new { order.Id }).ToToken();
            }
            catch (Exception ex)
            {
                FillErrors(ex, result);
            }
            return result;

        }

        private bool UpdateTotal(Order order, float total)
        {
            order = UnitOfWork.OrderRepository.Read(order.Id);
            order.Total = total;
            UnitOfWork.OrderRepository.Update(order);
            return UnitOfWork.Save() > 0;
        }

        private float AssingProductsToOrder(Order order, IEnumerable<Tuple<long, int>> products)
        {
            var productsId = products.Select(p => p.Item1);
            var sellerProduct = UnitOfWork.ProductRepository.FindBy(p => p.SellerId == order.SellerId &&
                                                                         productsId.Contains(p.Id));
            var productsInOrder = sellerProduct.Select(p => new OrderProduct
            {
                OrderId = order.Id,
                ProductId = p.Id,
                Quantity = products.First(q => q.Item1 == p.Id).Item2
            });

            float total = 0f;

            foreach (var productOrder in productsInOrder)
            {
                total += sellerProduct.First(p => p.Id == productOrder.ProductId).Price * productOrder.Quantity;
                UnitOfWork.OrderProductRepository.Create(productOrder);
            }

            UnitOfWork.Save();

            return total;
        }

        public Order CreateOrder(Guid userId, Guid sellerId, string note)
        {

            var order = new Order
            {
                Note = note,
                SellerId = sellerId,
                UserId = userId,
                StatusId = 1
            };

            order = UnitOfWork.OrderRepository.Create(order);

            UnitOfWork.Save();

            return order;
        }
    }
}
