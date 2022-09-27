using AutoMapper;
using NetKubernet.Dtos.PropertyDtos;

using NetKubernet.Models;

namespace NetKubernet.Profiles;

public class PropertyProfile : Profile
{
    public PropertyProfile()
    {
        CreateMap<Property, PropertyResponseDto>();
        CreateMap<PropertyRequestDto, Property>();
    }
}