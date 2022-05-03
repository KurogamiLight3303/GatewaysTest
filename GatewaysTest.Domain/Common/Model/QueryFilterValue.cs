namespace GatewaysTest.Domain.Common.Model;

public class QueryFilterValue
{
    public string? Alias { get; set; }
    public object? Value { get; set; }
    public QueryFilterType Type { get; set; }
}