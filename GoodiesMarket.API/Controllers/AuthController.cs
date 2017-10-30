using System.Web.Http;

namespace GoodiesMarket.API.Controllers
{
    public class AuthController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok(new { message = "woot" });
        }
    }
}
