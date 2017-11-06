using GoodiesMarket.Data.Contracts;

namespace GoodiesMarket.Business.Processes
{
    public class AccountProcess : ProcessBase
    {
        public AccountProcess(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
