using System.Data.Entity;

namespace GoodiesMarket.Model.Migrations
{
    class Initializer : DropCreateDatabaseIfModelChanges<GoodiesMarketContext>
    {
        protected override void Seed(GoodiesMarketContext context)
        {
            context.Statuses.Add(new Status
            {
                Description = "Pendiente"
            });
            context.Statuses.Add(new Status
            {
                Description = "En Progreso"
            });
            context.Statuses.Add(new Status
            {
                Description = "Cancelada"
            });
            context.Statuses.Add(new Status
            {
                Description = "Retrasada"
            });
            context.Statuses.Add(new Status
            {
                Description = "Completada"
            });
            base.Seed(context);
        }
    }
}
