using GoodiesMarket.API.Extensions;
using GoodiesMarket.API.Models;
using GoodiesMarket.Business.Processes;
using GoodiesMarket.Components.Models;
using GoodiesMarket.Data.Contracts;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace GoodiesMarket.API.Controllers
{
    public class AccountController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Authorize(Roles = "Seller, Buyer")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(new { id = User.Identity.GetUserId(), roles = User.Identity.GetRoles() });
        }

        public async Task<IHttpActionResult> Register([FromBody]JToken token)
        {
            var model = token.ToObject<Register>();

            var process = new AccountProcess(unitOfWork);

            Result result = await process.Register(model.Name, model.Email, model.Password, model.RoleType);

            return GetErrorResult(result) ?? Ok(result);
        }
    }
}