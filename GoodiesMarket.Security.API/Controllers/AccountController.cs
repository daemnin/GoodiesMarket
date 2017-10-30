using GoodiesMarket.Security.API.Models;
using GoodiesMarket.Security.Data.Repository;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Http;

namespace GoodiesMarket.Security.API.Controllers
{
    public class AccountController : ApiController
    {
        ISecurityRepository repository;

        public AccountController(ISecurityRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IHttpActionResult> Register(Login model)
        {
            if (!ModelState.IsValid) return BadRequest();

            IdentityResult result = await repository.CreateUser(model.Username, model.Password);

            return GetErrorResult(result) ?? Ok();
        }

        #region Support Methods
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                return BadRequest(ModelState);
            }

            return null;
        }
        #endregion
    }
}
