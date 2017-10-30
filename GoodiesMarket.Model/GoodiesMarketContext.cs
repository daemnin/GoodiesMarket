using System.Data.Entity;

namespace GoodiesMarket.Model
{
    public class GoodiesMarketContext : DbContext
    {
        public GoodiesMarketContext()
            : base("GoodiesMarketContext")
        {
        }
    }
}