using AutoMapper;
using GatewaysTest.Domain.Model.Gateways;

namespace GatewaysTest.Domain.Core.Features.Gateways;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<GatewayObject, GatewayResume>();
        CreateMap<GatewayObject, GatewayDetailResume>()
            .ForMember(p => p.Peripherals, p 
            => p.MapFrom(s => s.Items ?? new List<PeripheralObject>()))
            .IncludeBase<GatewayObject, GatewayResume>();
    }
}