using GoodiesMarket.Model;
using System;

namespace GoodiesMarket.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> UserRepository { get; }
        IRepository<Seller> SellerRepository { get; }
        IRepository<Product> ProductRepository { get; }
        IRepository<Order> OrderRepository { get; }
        IRepository<OrderProduct> OrderProductRepository { get; }

        int Save();
    }
}
