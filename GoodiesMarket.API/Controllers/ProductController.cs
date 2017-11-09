using GoodiesMarket.API.Extensions;
using GoodiesMarket.API.Models;
using GoodiesMarket.Business.Processes;
using GoodiesMarket.Components.Models;
using GoodiesMarket.Data.Contracts;
using System.Web.Http;

namespace GoodiesMarket.API.Controllers
{
    public class ProductController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize(Roles = "Buyer")]
        public IHttpActionResult Search([FromUri] Search search)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var process = new ProductProcess(unitOfWork);

            Result result = process.Search(User.Identity.GetId(), search.Query);

            return GetErrorResult(result) ?? Ok(result);
        }
    }
}
