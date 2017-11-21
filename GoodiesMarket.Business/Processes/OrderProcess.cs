using GoodiesMarket.Components.Extensions;
using GoodiesMarket.Components.Models;
using GoodiesMarket.Data.Contracts;
using GoodiesMarket.Model;
using Newtonsoft.Json.Linq;
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

        public Result Get(Guid id, RoleType role)
        {
            var result = new Result();

            try
            {
                switch (role)
                {
                    case RoleType.Buyer:
                        result.Response = GetBuyerOrders(id);
                        break;
                    case RoleType.Seller:
                        result.Response = GetSellerOrders(id);
                        break;
                }

                result.Succeeded = true;
            }
            catch (Exception ex)
            {
                FillErrors(ex, result);
            }

            return result;
        }

        public Result ChangeStatus(Guid userId, long id, StatusType status)
        {
            var result = new Result();

            try
            {
                var order = UnitOfWork.OrderRepository.Read(id);

                if (order == null) throw new Exception("Order inválida.");

                if (!(order.SellerId.Equals(userId) || order.UserId.Equals(userId)))
                    throw new Exception("Usted no tiene permisos para editar esta orden.");

                if (order.StatusId == (int)StatusType.Cancelled || order.StatusId == (int)StatusType.Completed)
                    throw new Exception("La orden ya no puede ser modificada.");

                order.StatusId = (int)status;

                UnitOfWork.OrderRepository.Update(order);

                if (status == StatusType.Cancelled)
                {
                    Rollback(id);
                }

                result.Succeeded = UnitOfWork.Save() > 0;
            }
            catch (Exception ex)
            {
                FillErrors(ex, result);
            }

            return result;
        }

        public Result GetBuyerLocation(Guid sellerId, long id)
        {
            var result = new Result();

            try
            {
                if (!UnitOfWork.OrderRepository.Any(o => o.SellerId.Equals(sellerId) && o.Id == id))
                    throw new Exception("Orden inválida.");

                var order = UnitOfWork.OrderRepository.Read(id, o => o.User, o => o.Status);

                if (((StatusType)order.StatusId) != StatusType.InProgress)
                    throw new Exception("Esta orden ya ha sido atendida.");

                result.Response = new
                {
                    order.User.Latitude,
                    order.User.Longitude
                }.ToToken();

                result.Succeeded = true;

            }
            catch (Exception ex)
            {
                FillErrors(ex, result);
            }

            return result;
        }

        private void Rollback(long id)
        {
            var order = UnitOfWork.OrderRepository.Read(id, o => o.Products);

            foreach (var productInOrder in order.Products)
            {
                var product = UnitOfWork.ProductRepository.Read(productInOrder.ProductId);

                product.Stock += productInOrder.Quantity;

                UnitOfWork.ProductRepository.Update(product);
            }
        }

        public Result Get(long id)
        {
            var result = new Result();

            try
            {
                var order = UnitOfWork.OrderRepository.Read(id,
                                                            o => o.Seller.User,
                                                            o => o.User,
                                                            o => o.Products,
                                                            o => o.Products.Select(p => p.Product));

                result.Response = (new
                {
                    order.Id,
                    order.Note,
                    order.StatusId,
                    order.Total,
                    order.CreatedOn,
                    order.LastModification,
                    Buyer = order.User.Name,
                    Seller = order.Seller.User.Name,
                    Products = order.Products.Select(p => new
                    {
                        Id = p.ProductId,
                        p.Product.Name,
                        p.Quantity
                    })
                }).ToToken();

                result.Succeeded = true;
            }
            catch (Exception ex)
            {
                FillErrors(ex, result);
            }

            return result;
        }

        private JToken GetBuyerOrders(Guid id)
        {
            var orders = UnitOfWork.OrderRepository.FindBy(o => o.UserId.Equals(id),
                                                           o => o.Seller.User);
            return orders.Select(o => new
            {
                o.Id,
                Seller = o.Seller.User.Name,
                Status = o.StatusId,
                o.Total
            }).OrderBy(o => o.Status).ToToken();
        }

        private JToken GetSellerOrders(Guid id)
        {
            var orders = UnitOfWork.OrderRepository.FindBy(o => o.SellerId.Equals(id),
                                                           o => o.User);
            return orders.Select(o => new
            {
                o.Id,
                Buyer = o.User.Name,
                Status = o.StatusId,
                o.Total
            }).OrderBy(o => o.Status).ToToken();
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
                var product = UnitOfWork.ProductRepository.Read(productOrder.ProductId);
                product.Stock -= productOrder.Quantity;
                UnitOfWork.ProductRepository.Update(product);
            }

            UnitOfWork.Save();

            return total;
        }

        private Order CreateOrder(Guid userId, Guid sellerId, string note)
        {

            var order = new Order
            {
                Note = note,
                SellerId = sellerId,
                UserId = userId,
                StatusId = 1,
                CreatedOn = DateTime.UtcNow,
                LastModification = DateTime.UtcNow
            };

            order = UnitOfWork.OrderRepository.Create(order);

            UnitOfWork.Save();

            return order;
        }
    }
}
