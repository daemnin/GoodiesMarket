using Microsoft.AspNet.Identity.EntityFramework;
using System;
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
                Name = "Goodies Market App",
                Active = true,
                AllowedOrigin = "*",
                RefreshTokenLifeTime = 720,
                ApplicationType = ApplicationType.NativeConfidential,
                Secret = "SY/weuz8yB0YpYbJfcQW52stzFs9SQUGhWKYPy/BaK0="
                //-,RN#40<C3uH;x?EtMv9-~yK!ayRtovA_ke-|AqDPsLMZ(x;_-%0oEM2Kvu}jW+x
            });

            context.Roles.Add(new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Seller"
            });
            context.Roles.Add(new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Buyer"
            });

            base.Seed(context);
        }

    }
}
