using GoodiesMarket.Model;
using System;

namespace GoodiesMarket.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> UserRepository { get; }
        IRepository<Seller> SellerRepository { get; }

        int Save();
    }
}
