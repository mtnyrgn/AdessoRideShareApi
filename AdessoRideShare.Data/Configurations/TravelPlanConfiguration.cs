using System;
using AdessoRideShare.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdessoRideShare.Data.Configurations
{
    public class TravelPlanConfiguration : IEntityTypeConfiguration<TravelPlan>
    {
        public void Configure(EntityTypeBuilder<TravelPlan> builder)
        {
            builder.HasKey(hk => hk.Id);
            builder.HasOne(ho => ho.User).WithMany(wm => wm.TravelPlans).HasForeignKey(fk => fk.UserId);

            builder.ToTable("RideShare_TravelPlan");
        }
    }
}
