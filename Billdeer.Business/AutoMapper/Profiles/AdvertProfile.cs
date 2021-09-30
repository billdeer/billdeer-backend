using AutoMapper;
using Billdeer.Business.Handlers.Adverts.Commands;
using Billdeer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Business.AutoMapper.Profiles
{
   public class AdvertProfile : Profile
    {
        public AdvertProfile()
        {
            CreateMap<CreateAdvertCommand, Advert>()
               .ForMember(
                   dest => dest.CreatedDate,
                   opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(
                   dest => dest.IsActive,
                   opt => opt.MapFrom(src => true))
               .ForMember(
                   dest => dest.IsDeleted,
                   opt => opt.MapFrom(src => false));

            CreateMap<UpdateAdvertCommand, Advert>()
               .ForMember(
                   dest => dest.ModifiedDate,
                   opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
