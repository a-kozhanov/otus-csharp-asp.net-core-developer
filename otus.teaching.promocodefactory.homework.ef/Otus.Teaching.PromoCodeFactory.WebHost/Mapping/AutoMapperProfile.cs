using AutoMapper;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerShortResponse>();
            CreateMap<Customer, CustomerResponse>()
                .ForMember(dest => dest.PromoCodes, opt => opt.MapFrom(src => src.PromoCodes));

            CreateMap<Preference, PreferenceResponse>();
            CreateMap<PromoCode, PromoCodeShortResponse>();
            CreateMap<CreateOrEditCustomerRequest, Customer>();
            CreateMap<Employee, EmployeeShortResponse>();
            CreateMap<Employee, EmployeeResponse>()
                .ForMember(x => x.Role, dest => dest.MapFrom(src => src.Role));
            CreateMap<Role, RoleItemResponse>();
            
            CreateMap<GivePromoCodeRequest, PromoCode>()
                .ForMember(dest => dest.ServiceInfo, opt => opt.MapFrom(src => src.ServiceInfo))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.PromoCode))
                .ForMember(dest => dest.PartnerName, opt => opt.MapFrom(src => src.PartnerName))
                .ForMember(dest => dest.Preference, opt => opt.Ignore());
        }
    }
}