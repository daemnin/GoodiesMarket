using GoodiesMarket.Data.Contracts;

namespace GoodiesMarket.API.Controllers
{
    public class FavoriteController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public FavoriteController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

    }
}
