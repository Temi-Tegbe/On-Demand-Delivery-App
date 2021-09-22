using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OnDemandDeliveryApp.Domain.Entitities;

namespace OnDemandDeliveryApp.Infrastructure
{
    public class OnDemandDeliveryDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long>
    {
        public OnDemandDeliveryDbContext(DbContextOptions<OnDemandDeliveryDbContext> options) : base(options)

        {


        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
             .Property("Id").UseIdentityColumn();

            builder.Entity<ApplicationRole>()
                .Property("Id").UseIdentityColumn();


            builder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne();

            builder.Entity<Dispatcher>()
                .HasOne(d => d.User)
                .WithOne();

            builder.Entity<DispatchedProduct>().HasKey(dp => new { dp.DispatcherId, dp.ProductId });

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Dispatcher> Dispatchers { get; set; }

    }
}
