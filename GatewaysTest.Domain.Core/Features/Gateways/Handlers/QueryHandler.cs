using System.Linq.Expressions;
using AutoMapper;
using GatewaysTest.Domain.Common.Language;
using GatewaysTest.Domain.Common.Model;
using GatewaysTest.Domain.Core.Common.Queries;
using GatewaysTest.Domain.Core.Features.Gateways.Queries;
using GatewaysTest.Domain.Model.Gateways;
using GatewaysTest.Domain.Repositories;

namespace GatewaysTest.Domain.Core.Features.Gateways.Handlers;

public class QueryHandler :
    IPagedQueryHandler<SearchGatewaysQuery, GatewayResume>,
    IQueryHandler<GetGatewayQuery, GatewayDetailResume>
{
    private readonly IGatewayQueryRepository _repository;
    private readonly IMapper _mapper;

    public QueryHandler(IGatewayQueryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedResultValue<GatewayResume>> Handle(SearchGatewaysQuery request, CancellationToken cancellationToken)
    {
        return await _repository.SearchAsync<GatewayResume>(request, cancellationToken);
    }

    public async Task<OperationResultValue<GatewayDetailResume>> Handle(GetGatewayQuery request, CancellationToken cancellationToken)
    {
        GatewayObject? gateway;
        if ((gateway = await _repository.FindAsync(p => p.SerialNo == request.SerialNo,
                new Expression<Func<GatewayObject, object>>[]{ p => p.Items! },
                cancellationToken)) == null) return new(true, I18n.GatewayNotFound);
        return new(true, _mapper.Map<GatewayDetailResume>(gateway));
    }
}