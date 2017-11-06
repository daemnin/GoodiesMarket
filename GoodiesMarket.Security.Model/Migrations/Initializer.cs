using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GoodiesMarket.Security.Model.Migrations
{
    public class Initializer : DropCreateDatabaseIfModelChanges<GoodiesMarketAuthContext>
    {
        protected override void Seed(GoodiesMarketAuthContext context)
        {
            context.Clients.Add(new Client
            {
                Id = "64E7D735-EC3C-48CB-B877-46F6A89775D4",
                Name = "Goodies Market API",
                Active = true,
                AllowedOrigin = "*",
                RefreshTokenLifeTime = 720,
                ApplicationType = ApplicationType.NativeConfidential,
                Secret = "SY/weuz8yB0YpYbJfcQW52stzFs9SQUGhWKYPy/BaK0="
                //-,RN#40<C3uH;x?EtMv9-~yK!ayRtovA_ke-|AqDPsLMZ(x;_-%0oEM2Kvu}jW+x
            });
            context.Clients.Add(new Client
            {
                Id = "9911979C-CE6A-4819-ABA2-8E880E6DE0D3",
                Name = "Goodies Market App",
                Active = true,
                AllowedOrigin = "*",
                RefreshTokenLifeTime = 720,
                ApplicationType = ApplicationType.JavaScript,
                Secret = "LHV7Riy7E9yeG+BdSG283GqhWW8J4MzsUTsTlcLfUKE="
                //#Bl=6/<)xo|;ZL;%.A2KX1>tV*J7OiMrVwzpT8rscTRxLHj`aBGm^8sg^(<GB)<J
            });

            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);

            manager.Create(new IdentityRole
            {
                Name = "Seller"
            });
            manager.Create(new IdentityRole
            {
                Name = "Buyer"
            });

            base.Seed(context);
        }

    }
}
