using AutoMapper;
using Misa.CRM.Business.Dtos.Customer;
using Misa.CRM.Business.Dtos.CustomerType;
using Misa.CRM.Business.Entities.Common;

namespace Misa.CRM.Business.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CreateMap<Source, Destination>();
        // Customer Mapping
        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.CustomerType, opt => opt.MapFrom(src => src.CustomerType != null ? src.CustomerType : null));

        CreateMap<CustomerCreateUpdateDto, Customer>()
            .ForMember(dest => dest.CustomerTypeId, opt => opt.MapFrom(src => src.CustomerTypeId != null ? src.CustomerTypeId : null));

        // CustomerType Mapping
        CreateMap<CustomerType, CustomerTypeDto>();
    }
}
