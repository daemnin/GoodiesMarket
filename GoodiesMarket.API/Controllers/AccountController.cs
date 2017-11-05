using System.Web.Http;

namespace GoodiesMarket.API.Controllers
{
    public class AccountController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok(new { message = "woot" });
        }
    }
}
