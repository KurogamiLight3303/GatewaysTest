using GatewaysTest.Domain.Common.Repositories;
using GatewaysTest.Domain.Model.Gateways;

namespace GatewaysTest.Domain.Repositories;

public interface IGatewayQueryRepository : IDomainQueryRepository<GatewayObject, Guid>
{
    
}