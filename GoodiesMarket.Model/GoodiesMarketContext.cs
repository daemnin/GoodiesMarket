using GoodiesMarket.Model.Contracts;
using System.Data.Entity;

namespace GoodiesMarket.Model
{
    public class GoodiesMarketContext : DbContext, IContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public GoodiesMarketContext()
            : base("GoodiesMarketContext")
        {
        }
    }
}