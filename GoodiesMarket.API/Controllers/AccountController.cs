using System.Web.Http;

namespace GoodiesMarket.API.Controllers
{
    public class AccountController : ApiController
    {
        public IHttpActionResult Register()
        {
            return Ok(new { message = "woot" });
        }
    }
}
