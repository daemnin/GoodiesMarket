using GoodiesMarket.Data.Contracts;
using GoodiesMarket.Model;
using GoodiesMarket.Model.Contracts;
using System;

namespace GoodiesMarket.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private IContext context;
        private bool disposed;

        public UnitOfWork(IContext context)
        {
            this.context = context;
        }

        private IRepository<User> userRepository;
        public IRepository<User> UserRepository
        {
            get { return userRepository ?? (userRepository = new Repository<User>(context)); }
        }

        private IRepository<Seller> sellerRepository;
        public IRepository<Seller> SellerRepository
        {
            get { return sellerRepository ?? (sellerRepository = new Repository<Seller>(context)); }
        }

        private IRepository<Product> productRepository;
        public IRepository<Product> ProductRepository
        {
            get { return productRepository ?? (productRepository = new Repository<Product>(context)); }
        }

        private IRepository<Order> orderRepository;
        public IRepository<Order> OrderRepository
        {
            get { return orderRepository ?? (orderRepository = new Repository<Order>(context)); }
        }

        private IRepository<OrderProduct> orderProductRepository;
        public IRepository<OrderProduct> OrderProductRepository
        {
            get { return orderProductRepository ?? (orderProductRepository = new Repository<OrderProduct>(context)); }
        }


        public void Dispose()
        {
            if (!disposed)
            {
                context.Dispose();
                GC.SuppressFinalize(this);
                disposed = true;
            }
        }

        public int Save()
        {
            int changes = 0;

            if (!disposed && context.ChangeTracker.HasChanges())
            {
                changes = context.SaveChanges();
            }

            return changes;
        }
    }
}
