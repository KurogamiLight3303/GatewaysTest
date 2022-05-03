using System.Linq.Expressions;
using AutoMapper;
using GatewaysTest.Domain.Common.Language;
using GatewaysTest.Domain.Common.Model;
using GatewaysTest.Domain.Core.Common.Queries;
using GatewaysTest.Domain.Core.Features.Peripherals.Queries;
using GatewaysTest.Domain.Model.Gateways;
using GatewaysTest.Domain.Repositories;

namespace GatewaysTest.Domain.Core.Features.Peripherals.Handlers;

public class QueryHandler : ICollectionQueryHandler<ListPeripheralsQuery, PeripheralResume>
{
    private readonly IGatewayQueryRepository _repository;
    private readonly IMapper _mapper;

    public QueryHandler(IGatewayQueryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<QueryResult<PeripheralResume>> Handle(ListPeripheralsQuery request, CancellationToken cancellationToken)
    {
        GatewayObject? gateway;
        if ((gateway = await _repository.FindAsync(p => p.SerialNo == request.SerialNo,
                new Expression<Func<GatewayObject, object>>[]{ p => p.Items! },
                cancellationToken)) == null) return new(I18n.GatewayNotFound);
        return new(_mapper.Map<PeripheralResume[]>(gateway.Items));
    }
}