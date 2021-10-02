using System;
using AdessoRideShare.Core.Entities;
using AdessoRideShare.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AdessoRideShare.Data.Contexts
{
    public class AdessoDbContext : DbContext
    {
        public AdessoDbContext(DbContextOptions<AdessoDbContext> options) : base(options)

        {
        }

        public DbSet<TravelPlan> TravelPlans { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new TravelPlanConfiguration());
        }
    }
}
