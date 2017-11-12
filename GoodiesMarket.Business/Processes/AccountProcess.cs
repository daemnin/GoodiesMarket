using GoodiesMarket.Business.Proxies;
using GoodiesMarket.Components.Extensions;
using GoodiesMarket.Components.Models;
using GoodiesMarket.Data.Contracts;
using GoodiesMarket.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GoodiesMarket.Business.Processes
{
    public class AccountProcess : ProcessBase
    {
        public AccountProcess(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<Result> Register(string name, string email, string password, RoleType roleType)
        {
            var result = new Result();

            try
            {
                var exists = UnitOfWork.UserRepository.Any(u => u.Email.Equals(email.ToLower()));

                if (exists) throw new Exception("Email already registered");

                var securityProxy = new SecurityProxy();

                var registerResult = await securityProxy.Register(email, password, roleType);

                if (!registerResult.Succeeded) return registerResult;

                var userId = registerResult.Response.Value<string>("userId");

                result.Succeeded = CreateUser(userId, name, email, roleType);
            }
            catch (Exception ex)
            {
                FillErrors(ex, result);
            }

            return result;
        }

        public Result Get(Guid userId, RoleType role)
        {
            var result = new Result();

            try
            {
                switch (role)
                {
                    case RoleType.Buyer:
                        result.Response = GetBuyerProfile(userId);
                        break;
                    case RoleType.Seller:
                        result.Response = GetSellerProfile(userId);
                        break;
                    default:
                        throw new InvalidOperationException("Invalid role");
                }

                result.Succeeded = true;
            }
            catch (Exception ex)
            {
                FillErrors(ex, result);
            }

            return result;
        }

        public Result Update(Guid userId, RoleType role, double? latitude, double? longitude, int? range)
        {
            var result = new Result();

            try
            {
                var user = UnitOfWork.UserRepository.Read(userId);

                if (latitude.HasValue && longitude.HasValue)
                {
                    user.Latitude = latitude;
                    user.Longitude = longitude;
                }
                if (role == RoleType.Seller && range.HasValue)
                {
                    var seller = UnitOfWork.SellerRepository.Read(userId);

                    seller.Range = range.Value;

                    UnitOfWork.SellerRepository.Update(seller);
                }

                UnitOfWork.UserRepository.Update(user);

                result.Succeeded = UnitOfWork.Save() > 0;
            }
            catch (Exception ex)
            {
                FillErrors(ex, result);
            }

            return result;
        }

        #region Support Methods
        private JToken GetBuyerProfile(Guid id)
        {
            return UnitOfWork.UserRepository.FindBy(u => u.Id.Equals(id))
                    .Select(u => new
                    {
                        u.Name,
                        u.PictureUrl,
                        u.Email,
                        u.Score,
                        u.Latitude,
                        u.Longitude
                    })
                    .FirstOrDefault()
                    .ToToken();
        }

        private JToken GetSellerProfile(Guid id)
        {
            return UnitOfWork.SellerRepository.FindBy(s => s.Id.Equals(id), s => s.User, s => s.Products)
                    .Select(s => new
                    {
                        s.User.Name,
                        s.User.PictureUrl,
                        s.User.Email,
                        s.User.Score,
                        s.User.Latitude,
                        s.User.Longitude,
                        s.Range,
                        s.Motto,
                        s.Restriction,
                        Products = s.Products.Select(p => new
                        {
                            p.Id,
                            p.Name,
                            p.Description,
                            p.Price,
                            p.Stock,
                            p.ImageUrl
                        })
                    })
                    .FirstOrDefault()
                    .ToToken();
        }

        private bool CreateUser(string userId, string name, string email, RoleType roleType)
        {
            var user = new User
            {
                Id = Guid.Parse(userId),
                Name = name,
                Email = email.ToLower()
            };

            UnitOfWork.UserRepository.Create(user);

            if (roleType == RoleType.Seller)
            {
                var seller = new Seller
                {
                    Id = Guid.Parse(userId)
                };

                UnitOfWork.SellerRepository.Create(seller);
            }

            return UnitOfWork.Save() > 0;
        }
        #endregion

    }
}
