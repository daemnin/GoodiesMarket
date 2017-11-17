using GoodiesMarket.Components.Extensions;
using GoodiesMarket.Components.Helpers;
using GoodiesMarket.Components.Models;
using GoodiesMarket.Data.Contracts;
using GoodiesMarket.Model;
using System;
using System.Linq;

namespace GoodiesMarket.Business.Processes
{
    public class ProductProcess : ProcessBase
    {
        public ProductProcess(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public Result Search(Guid userId, string productName)
        {
            var result = new Result();

            try
            {
                var user = UnitOfWork.UserRepository.Read(userId);

                if (user.Latitude == null || user.Longitude == null) throw new Exception("El usuario no ha definido su ubicación.");

                var sellers = UnitOfWork.SellerRepository
                                .FindBy(s => s.Products.Any(p => p.Description.Contains(productName)) &&
                                             s.User.Latitude != null &&
                                             s.User.Longitude != null &&
                                             s.Range != null,
                                             s => s.User);

                var nearBySellers = sellers.AsParallel().Where(seller => InRange(user, seller));

                result.Response = nearBySellers.Select(s => new
                {
                    s.Id,
                    s.User.Name,
                    s.User.Score
                }).ToToken();

                result.Succeeded = true;
            }
            catch (Exception ex)
            {
                FillErrors(ex, result);
            }

            return result;
        }

        public Result Delete(long productId)
        {
            var result = new Result();
            try
            {
                UnitOfWork.ProductRepository.Delete(productId);

                result.Succeeded = UnitOfWork.Save() > 0;
            }
            catch (Exception ex)
            {

                FillErrors(ex, result);
            }


            return result;
        }

        public Result Update(long productId, string name, string description, float price, int? stock)
        {
            var result = new Result();
            try
            {
                var product = UnitOfWork.ProductRepository.Read(productId);

                product.Name = name;
                product.Description = description;
                product.Price = price;
                product.Stock = stock;

                UnitOfWork.ProductRepository.Update(product);

                result.Succeeded = UnitOfWork.Save() > 0;
            }
            catch (Exception ex)
            {
                FillErrors(ex, result);
            }
            return result;
        }

        public Result Create(Guid sellerId, string name, string description, float price, int? stock)
        {
            var result = new Result();
            try
            {
                var product = CreateProduct(sellerId, name, description, price, stock);

                result.Succeeded = product.Id > 0;

                if (result.Succeeded)
                {
                    result.Response = (new { product.Id }).ToToken();
                }
            }
            catch (Exception ex)
            {
                FillErrors(ex, result);
            }
            return result;

        }

        public Result GetProducts(Guid sellerId)
        {
            var result = new Result();

            try
            {
                var seller = UnitOfWork.SellerRepository.Read(sellerId, s => s.Products);

                result.Response = (seller.Products.Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Price,
                    p.Stock,
                    p.ImageUrl
                })).ToToken();

                result.Succeeded = true;
            }
            catch (Exception ex)
            {
                FillErrors(ex, result);
            }

            return result;
        }

        private Product CreateProduct(Guid sellerId, string name, string description, float price, int? stock)
        {
            stock = stock == 0 ? null : stock;
            var product = new Product
            {
                SellerId = sellerId,
                Name = name,
                Description = description,
                Price = price,
                Stock = stock
            };



            product = UnitOfWork.ProductRepository.Create(product);

            UnitOfWork.Save();

            return product;
        }

        private bool InRange(User user, Seller seller)
        {
            double distance = DistanceHelper.Instance.Calculate(user.Latitude.Value, user.Longitude.Value, seller.User.Latitude.Value, seller.User.Longitude.Value);

            return distance <= seller.Range;
        }
    }
}