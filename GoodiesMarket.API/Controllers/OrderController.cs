using GoodiesMarket.API.Extensions;
using GoodiesMarket.API.Models;
using GoodiesMarket.Business.Processes;
using GoodiesMarket.Components.Models;
using GoodiesMarket.Data.Contracts;
using System;
using System.Linq;
using System.Web.Http;

namespace GoodiesMarket.API.Controllers
{
    public class OrderController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IHttpActionResult Create([FromBody]Order order)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var process = new OrderProcess(unitOfWork);

            var products = order.Products.Select(p => new Tuple<long, int>(p.Id, p.Quantity));

            Result result = process.Create(User.Identity.GetId(), order.SellerId, order.Note, products);

            return GetErrorResult(result) ?? Ok(result);
        }
    }
}
