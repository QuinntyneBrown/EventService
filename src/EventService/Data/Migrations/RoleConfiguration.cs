using System.Data.Entity.Migrations;
using EventService.Data;
using EventService.Data.Model;
using EventService.Features.Users;

namespace EventService.Migrations
{
    public class RoleConfiguration
    {
        public static void Seed(EventServiceContext context) {

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.SYSTEM
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.CUSTOMER
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.DEVELOPMENT
            });

            context.SaveChanges();
        }
    }
}
