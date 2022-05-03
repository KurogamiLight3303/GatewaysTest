using GatewaysTest.Domain.Model.Gateways;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GatewaysTest.Infrastructure.Persistence.Configurations;

public class EntityTypeConfigurations : 
    IEntityTypeConfiguration<GatewayObject>,
    IEntityTypeConfiguration<PeripheralObject>
{

    public void Configure(EntityTypeBuilder<GatewayObject> builder)
    {
        builder.ConfigureAggregateDomainEntity<GatewayObject, Guid, Guid, PeripheralObject>("Gateways");
        builder
            .Property(p => p.SerialNo)
            .HasMaxLength(100);
        builder
            .Property(p => p.Name)
            .HasMaxLength(250);
        builder.Property(p => p.IpAddress)
            .HasConversion(
                p => p!.ToString(),
                p => new(p),
                new ValueComparer<V4IpAddress>(
                    (o1, o2) => o1 != null && o1.Equals(o2),
                    v => v.GetHashCode(),
                    v => new(v.ToString())));
    }

    public void Configure(EntityTypeBuilder<PeripheralObject> builder)
    {
        builder.ConfigureDomainEntity<PeripheralObject, Guid>("Peripherals");
        builder.Property(p => p.Vendor).HasMaxLength(250);
    }
}