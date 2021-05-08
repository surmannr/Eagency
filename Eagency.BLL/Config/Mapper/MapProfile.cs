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

            CreateMap<Contract, ContractDto>()
                .ForMember(e => e.AgentName, a => a.MapFrom(s => s.Property.User.FirstName + " " + s.Property.User.LastName))
                .ForMember(e => e.ClientName, a => a.MapFrom(s => s.User.FirstName + " " + s.User.LastName))
                .ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();

            CreateMap<Property, PropertyDto>()
                .ForMember(e => e.Street, a => a.MapFrom(s => s.Address.Street))
                .ForMember(e => e.City, a => a.MapFrom(s => s.Address.City))
                .ForMember(e => e.Country, a => a.MapFrom(s => s.Address.Country))
                .ForMember(e => e.ZipCode, a => a.MapFrom(s => s.Address.ZipCode))
                .ReverseMap();

        }
    }
}
