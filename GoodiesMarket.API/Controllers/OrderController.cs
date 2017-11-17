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

        [Authorize(Roles = "Buyer")]
        public IHttpActionResult Create([FromBody]Order order)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var process = new OrderProcess(unitOfWork);

            var products = order.Products.Select(p => new Tuple<long, int>(p.Id, p.Quantity));

            var result = process.Create(User.Identity.GetId(), order.SellerId, order.Note, products);

            return GetErrorResult(result) ?? Ok(result);
        }

        [Authorize(Roles = "Buyer, Seller")]
        public IHttpActionResult Get()
        {
            var process = new OrderProcess(unitOfWork);

            var result = process.Get(User.Identity.GetId(), User.Identity.GetRole());

            return GetErrorResult(result) ?? Ok(result);
        }

        public IHttpActionResult Get(long id)
        {
            var process = new OrderProcess(unitOfWork);

            var result = process.Get(id);

            return GetErrorResult(result) ?? Ok(result);
        }

        [Authorize(Roles = "Buyer, Seller")]
        public IHttpActionResult Put(long id, [FromBody]UpdateStatus model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (User.Identity.GetRole() == RoleType.Buyer && model.Status != StatusType.Cancelled)
                return BadRequest("El estado de la orden es inválido.");

            var process = new OrderProcess(unitOfWork);

            var result = process.ChangeStatus(User.Identity.GetId(), id, model.Status);

            return GetErrorResult(result) ?? Ok(result);
        }
    }
}
