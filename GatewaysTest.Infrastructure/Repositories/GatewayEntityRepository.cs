using AutoMapper;
using GatewaysTest.Domain.Common.Model;
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
    public GatewayEntityRepository(
        DomainContext context, 
        IMapper mapper, 
        IQueryFilterTranslator<GatewayObject, Guid>? filterTranslator = null) 
        : base(context, mapper, filterTranslator)
    {
    }

    public Task<bool> ExistPeripheralAsync(int uid, CancellationToken cancellationToken = default) 
        => ChildDataSet.AnyAsync(p => p.UID == uid, cancellationToken);
}