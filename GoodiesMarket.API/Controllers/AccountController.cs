using GoodiesMarket.API.Extensions;
using GoodiesMarket.API.Models;
using GoodiesMarket.Business.Processes;
using GoodiesMarket.Components.Models;
using GoodiesMarket.Data.Contracts;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace GoodiesMarket.API.Controllers
{
    [Authorize(Roles = "Seller, Buyer")]
    public class AccountController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IHttpActionResult Get()
        {
            var process = new AccountProcess(unitOfWork);

            Result result = process.Get(User.Identity.GetId(), User.Identity.GetRole());

            return GetErrorResult(result) ?? Ok(result);
        }

        [HttpGet]
        [Route("api/account/role")]
        public IHttpActionResult Role()
        {
            
            return Ok(new { Role = User.Identity.GetRole() });
        }

        [HttpPut]
        public IHttpActionResult Update(Profile profile)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var process = new AccountProcess(unitOfWork);

            Result result = process.Update(User.Identity.GetId(), User.Identity.GetRole(), profile.Latitude, profile.Longitude, profile.Range, profile.Motto);

            return GetErrorResult(result) ?? Ok(result);
        }

        [AllowAnonymous]
        public async Task<IHttpActionResult> Register(Register register)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var process = new AccountProcess(unitOfWork);

            Result result = await process.Register(register.Name, register.Email, register.Password, register.RoleType);

            return GetErrorResult(result) ?? Ok(result);
        }
    }
}