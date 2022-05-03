using AutoMapper;
using GatewaysTest.Domain.Model.Gateways;

namespace GatewaysTest.Domain.Core.Features.Peripherals;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<PeripheralObject, PeripheralResume>()
            .ForMember(p => p.FabricationDate, p
                => p.MapFrom(s => s.FabricationDate.ToString("d")));
    }
}