using GoodiesMarket.Data.Contracts;

namespace GoodiesMarket.Business.Processes
{
    public abstract class ProcessBase
    {
        protected IUnitOfWork UnitOfWork { get; private set; }

        public ProcessBase(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
