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

                var sellers = UnitOfWork.SellerRepository
                                .FindBy(s => s.Products.Any(p => p.Description.Contains(productName)) &&
                                             s.User.Latitude != null &&
                                             s.User.Longitude != null &&
                                             s.Range != null,
                                             s => s.User);

                var nearBySellers = sellers.AsParallel().Where(seller => InRange(user, seller));

                result.Response = nearBySellers.ToToken();

                //var products = UnitOfWork.ProductRepository
                //                .FindBy(p => p.Description.Contains(name) &&
                //                             p.Seller.User.Latitude != null &&
                //                             p.Seller.User.Longitude != null &&
                //                             p.Seller.Range != null,
                //                             p => p.Seller, p => p.Seller.User);
                //var nearByProducts = products.AsParallel().Where(p => InRange(user, p.Seller));
                //result.Response = nearByProducts.ToToken();

                result.Succeeded = true;
            }
            catch (Exception ex)
            {
                FillErrors(ex, result);
            }

            return result;
        }

        private bool InRange(User user, Seller seller)
        {
            double distance = DistanceHelper.Instance.Calculate(user.Latitude.Value, user.Longitude.Value, seller.User.Latitude.Value, seller.User.Longitude.Value);

            return distance <= seller.Range;
        }
    }
}