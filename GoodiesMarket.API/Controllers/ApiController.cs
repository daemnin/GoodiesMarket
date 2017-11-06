using GoodiesMarket.Components.Models;

namespace GoodiesMarket.API.Controllers
{
    public abstract class ApiController : System.Web.Http.ApiController
    {
        protected System.Web.Http.IHttpActionResult GetErrorResult(Result result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}