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

        [HttpPut]
        [Authorize(Roles = "Seller")]
        public IHttpActionResult Update(long id, Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var process = new ProductProcess(unitOfWork);

            Result result = process.Update(id, product.Name,
                                           product.Description, product.Price, product.Stock);

            return GetErrorResult(result) ?? Ok(result);

        }

        [HttpDelete]
        [Authorize(Roles = "Seller")]
        public IHttpActionResult Delete(long id)
        {
            var process = new ProductProcess(unitOfWork);

            Result result = process.Delete(id);

            return GetErrorResult(result) ?? Ok(result);
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

        [Authorize(Roles = "Seller")]
        public IHttpActionResult Create(Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var process = new ProductProcess(unitOfWork);

            Result result = process.Create(User.Identity.GetId(), product.Name,
                                           product.Description, product.Price, product.Stock);

            return GetErrorResult(result) ?? Ok(result);
        }
    }
}
