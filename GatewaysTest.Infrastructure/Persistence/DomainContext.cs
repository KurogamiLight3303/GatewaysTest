using System.Reflection;
using GatewaysTest.Domain.Model.Gateways;
using Microsoft.EntityFrameworkCore;

namespace GatewaysTest.Infrastructure.Persistence;

public class DomainContext : DbContext
{
    public DbSet<GatewayObject>? Gateways { get; set; }
    public DbSet<PeripheralObject>? Peripherals { get; set; }
    public DomainContext(DbContextOptions<DomainContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}