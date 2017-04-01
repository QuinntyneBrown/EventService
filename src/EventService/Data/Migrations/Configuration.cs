namespace EventService.Migrations
{
    using Data;
    using Data.Helpers;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<EventServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EventServiceContext context)
        {
            TenantConfiguration.Seed(context);
            RoleConfiguration.Seed(context);
            UserConfiguration.Seed(context);
        }
    }

    public class DbConfiguration : System.Data.Entity.DbConfiguration
    {
        public DbConfiguration()
        {
            AddInterceptor(new SoftDeleteInterceptor());
        }
    }
}
