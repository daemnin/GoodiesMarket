using GoodiesMarket.Business.Proxies;
using GoodiesMarket.Components.Models;
using GoodiesMarket.Data.Contracts;
using GoodiesMarket.Model;
using System;
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
                var securityProxy = new SecurityProxy();

                var registerResult = await securityProxy.Register(email, password, roleType);

                if (!registerResult.Succeeded) return registerResult;

                var userId = registerResult.Response.Value<string>("userId");

                result.Succeeded = CreateUser(userId, name);
            }
            catch (Exception ex)
            {
                FillErrors(ex, result);
            }

            return result;
        }

        private bool CreateUser(string userId, string name)
        {
            var user = new User
            {
                Id = Guid.Parse(userId),
                Name = name
            };

            UnitOfWork.UserRepository.Create(user);

            return UnitOfWork.Save() > 0;
        }
    }
}
