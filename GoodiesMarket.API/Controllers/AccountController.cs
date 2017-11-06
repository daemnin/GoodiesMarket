using GoodiesMarket.API.Models;
using GoodiesMarket.Business.Processes;
using GoodiesMarket.Components.Models;
using GoodiesMarket.Data.Contracts;
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

        public async Task<IHttpActionResult> Register([FromBody]JToken token)
        {
            var model = token.ToObject<Register>();
            
            var process = new AccountProcess(unitOfWork);

            Result result = await process.Register(model.Name, model.Email, model.Password, model.RoleType);

            return GetErrorResult(result) ?? Ok(result);
        }
    }
}