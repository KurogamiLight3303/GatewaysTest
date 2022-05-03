using AutoMapper;
using GatewaysTest.Domain.Model.Gateways;
using GatewaysTest.Domain.Repositories;
using GatewaysTest.Infrastructure.Persistence;

namespace GatewaysTest.Infrastructure.Repositories;

public class GatewayEntityRepository : 
    AggregateBaseEntityRepository<GatewayObject, PeripheralObject, Guid, Guid>,
    IGatewayCommandRepository,
    IGatewayQueryRepository
{
    public GatewayEntityRepository(DomainContext context, IMapper mapper) : base(context, mapper)
    {
    }
}