namespace GatewaysTest.Domain.Common.Model;

public class QueryResult<TItem> : OperationResultValue
{
    public TItem[] Items { get; }
    public QueryResult(string? message) : base(false, message)
    {
        Items = Array.Empty<TItem>();
    }
    public QueryResult(TItem[] items) : base(true)
    {
        Items = items;
    }
}