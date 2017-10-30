using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GoodiesMarket.Security.Model
{
    public class GoodiesMarketAuthContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public GoodiesMarketAuthContext()
            : base("GoodiesMarketAuthContext")
        {

        }
    }
}
