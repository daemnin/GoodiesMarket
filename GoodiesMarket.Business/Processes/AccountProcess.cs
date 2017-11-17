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

        public Result Update(Guid userId, RoleType role, double? latitude, double? longitude, int? range, string motto)
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

                    seller.Motto = motto ?? seller.Motto;

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
            var profile = UnitOfWork.UserRepository.Read(id);

            return (new
            {
                profile.Name,
                profile.PictureUrl,
                profile.Email,
                profile.Score,
                profile.Latitude,
                profile.Longitude
            }).ToToken();
        }

        private JToken GetSellerProfile(Guid id)
        {
            var profile = UnitOfWork.SellerRepository.Read(id, s => s.User, s => s.Products);
            return (new
            {
                profile.User.Name,
                profile.User.PictureUrl,
                profile.User.Email,
                profile.User.Score,
                profile.User.Latitude,
                profile.User.Longitude,
                profile.Range,
                profile.Motto,
                profile.Restriction,
                Products = profile.Products.Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Price,
                    p.Stock,
                    p.ImageUrl
                })
            }).ToToken();
        }

        private bool CreateUser(string userId, string name, string email, RoleType roleType)
        {
            var user = new User
            {
                Id = Guid.Parse(userId),
                Name = name,
                Email = email.ToLower(),
                PictureUrl = "ic_profile"
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
