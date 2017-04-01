using System.Data.Entity.Migrations;
using EventService.Data;
using EventService.Data.Model;

namespace EventService.Migrations
{
    public class TenantConfiguration
    {
        public static void Seed(EventServiceContext context) {

            context.Tenants.AddOrUpdate(x => x.Name, new Tenant()
            {
                Name = "Default"
            });

            context.SaveChanges();
        }
    }
}
