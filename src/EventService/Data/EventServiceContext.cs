﻿using EventService.Data.Helpers;
using EventService.Data.Model;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;

namespace EventService.Data
{
    public interface IEventServiceContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Tenant> Tenants { get; set; }
        DbSet<DigitalAsset> DigitalAssets { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<Profile> Profiles { get; set; }
        Task<int> SaveChangesAsync();
    }

    public class EventServiceContext: DbContext, IEventServiceContext
    {
        public EventServiceContext()
            :base("EventServiceContext")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = true;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<DigitalAsset> DigitalAssets { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        public override int SaveChanges()
        {
            UpdateLoggableEntries();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            UpdateLoggableEntries();
            return base.SaveChangesAsync();
        }

        public void UpdateLoggableEntries()
        {
            foreach (var entity in ChangeTracker.Entries()
                .Where(e => e.Entity is ILoggable && ((e.State == EntityState.Added || (e.State == EntityState.Modified))))
                .Select(x => x.Entity as ILoggable))
            {
                entity.CreatedOn = entity.CreatedOn == default(DateTime) ? DateTime.UtcNow : entity.CreatedOn;
                entity.LastModifiedOn = DateTime.UtcNow;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().
                HasMany(u => u.Roles).
                WithMany(r => r.Users).
                Map(
                    m =>
                    {
                        m.MapLeftKey("User_Id");
                        m.MapRightKey("Role_Id");
                        m.ToTable("UserRoles");
                    });

            modelBuilder.Entity<Event>()
                        .HasOptional(e => e.EventLocation)
                        .WithRequired(el => el.Event);

            var convention = new AttributeToTableAnnotationConvention<SoftDeleteAttribute, string>(
                "SoftDeleteColumnName",
                (type, attributes) => attributes.Single().ColumnName);

            modelBuilder.Conventions.Add(convention);
        }
    }
}