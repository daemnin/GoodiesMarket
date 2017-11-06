using GoodiesMarket.Components.Models;
using GoodiesMarket.Data.Contracts;
using System;

namespace GoodiesMarket.Business.Processes
{
    public abstract class ProcessBase
    {
        protected IUnitOfWork UnitOfWork { get; private set; }

        public ProcessBase(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected void FillErrors(Exception exception, Result result)
        {
            while (exception != null)
            {
                result.Errors.Add(exception.Message);
                exception = exception.InnerException;
            }

            result.Succeeded = false;
        }
    }
}
