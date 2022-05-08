using AutoMapper;
using GatewaysTest.Domain.Model.Gateways;
using GatewaysTest.Domain.Repositories;
using GatewaysTest.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GatewaysTest.Infrastructure.Repositories;

public class GatewayEntityRepository : 
    AggregateBaseEntityRepository<GatewayObject, PeripheralObject, Guid, Guid>,
    IGatewayCommandRepository,
    IGatewayQueryRepository
{
    public GatewayEntityRepository(DomainContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public Task<bool> ExistPeripheralAsync(int uid, CancellationToken cancellationToken = default) 
        => Context.Peripherals.Where(p => p.UID == uid).AnyAsync(cancellationToken);
}