using GatewaysTest.Domain.Common.Model;

namespace GatewaysTest.Domain.Model.Gateways;

public class GatewayQueryFilterTranslator : IQueryFilterTranslator<GatewayObject, Guid>
{
    public Task<IQueryable<GatewayObject>> AddFiltersAsync(IQueryable<GatewayObject> query, 
        IEnumerable<QueryFilterValue> filters, CancellationToken cancellationToken)
    {
        var result = query;
        foreach (var filter in filters)
        {
            switch (filter.Alias)
            {
                case nameof(GatewayObject.SerialNo):
                    if (!string.IsNullOrEmpty(filter.Value))
                        result = result.Where(p => p.SerialNo!.StartsWith(filter.Value!));
                    break;
                case nameof(GatewayObject.Name):
                    if (!string.IsNullOrEmpty(filter.Value))
                        result = result.Where(p => p.Name!.StartsWith(filter.Value!));
                    break;
            }
        }

        return Task.FromResult(result);
    }
}