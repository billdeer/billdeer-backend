using AutoMapper;
using Billdeer.Business.Handlers.AdvertPackages.Commands;
using Billdeer.Business.Handlers.Adverts.Commands;
using Billdeer.Entities.Concrete;
using System;

namespace Billdeer.Business.AutoMapper.Profiles
{
    public class AdvertPackageProfile : Profile
    {
        public AdvertPackageProfile()
        {
            CreateMap<CreateAdvertPackageCommand, AdvertPackage>()
               .ForMember(
                   dest => dest.CreatedDate,
                   opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(
                   dest => dest.IsActive,
                   opt => opt.MapFrom(src => true))
               .ForMember(
                   dest => dest.IsDeleted,
                   opt => opt.MapFrom(src => false));

        }
    }
}
