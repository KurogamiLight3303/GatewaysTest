using GatewaysTest.Domain.Common.Model;

namespace GatewaysTest.Domain.Model.Gateways;

public class V4IpAddress : DomainValue
{
    public V4IpAddress(string source)
    {
        var data = source.Split('.').Select(int.Parse).ToArray();
        Octet1 = (byte) data[0];
        Octet2 = (byte) data[1];
        Octet3 = (byte) data[2];
        Octet4 = (byte) data[3];
    }
    public byte Octet1 { get; }
    public byte Octet2 { get; }
    public byte Octet3 { get; }
    public byte Octet4 { get; }

    protected override IEnumerable<object> GetCompareFields()
    {
        yield return Octet1;
        yield return Octet2;
        yield return Octet3;
        yield return Octet4;
    }

    public override string ToString() => $"{Octet1}.{Octet2}.{Octet3}.{Octet4}";

    public static bool IsValid(string? source)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(source))
                return false;
            var data = source
                .Split('.')
                .Select(int.Parse)
                .ToArray();
            return data
                .All(i => i is >= 0 and <= 255) && data.Length == 4;
        }
        catch
        {
            return false;
        }
    }
}