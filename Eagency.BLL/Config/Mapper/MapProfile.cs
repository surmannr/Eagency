using AutoMapper;
using Eagency.Dal.Entities;
using Eagency.Web.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.BLL.Config.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Contract, ContractDto>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();

            CreateMap<Property, PropertyDto>()
                .ForMember(e => e.Street, a => a.MapFrom(s => s.Address.Street))
                .ForMember(e => e.City, a => a.MapFrom(s => s.Address.City))
                .ForMember(e => e.Country, a => a.MapFrom(s => s.Address.Country))
                .ForMember(e => e.ZipCode, a => a.MapFrom(s => s.Address.ZipCode));

            CreateMap<PropertyDto, Property>()
                .BeforeMap((s,d) => d.Address.PropertyId = s.Id)
                .ForMember(e => e.Address.Street, a => a.MapFrom(s => s.Street))
                .ForMember(e => e.Address.City, a => a.MapFrom(s => s.City))
                .ForMember(e => e.Address.Country, a => a.MapFrom(s => s.Country))
                .ForMember(e => e.Address.ZipCode, a => a.MapFrom(s => s.ZipCode));
        }
    }
}
