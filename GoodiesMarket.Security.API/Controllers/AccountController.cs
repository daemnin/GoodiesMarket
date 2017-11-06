using GoodiesMarket.Security.API.Models;
using GoodiesMarket.Security.Data.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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

        public async Task<IHttpActionResult> Register(Register model)
        {
            if (!ModelState.IsValid) return BadRequest();

            IdentityResult result = await repository.CreateUser(model.Email, model.Password);

            if (!result.Succeeded) return GetErrorResult(result);

            result = await repository.AssignRole(model.Email, model.Password, model.RoleType.ToString());

            IdentityUser user = await repository.FindUser(model.Email, model.Password);

            return GetErrorResult(result) ?? Ok(new { userId = user.Id });
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
