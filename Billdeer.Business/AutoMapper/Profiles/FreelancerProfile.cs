using AutoMapper;
using Billdeer.Business.Handlers.Freelancers.Commands;
using Billdeer.Entities.Concrete;
using System;

namespace Billdeer.Business.AutoMapper.Profiles
{
    public class FreelancerProfile : Profile
    {
        public FreelancerProfile()
        {
            CreateMap<CreateFreelancerCommand, Freelancer>()
               .ForMember(
                   dest => dest.CreatedDate,
                   opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(
                   dest => dest.IsActive,
                   opt => opt.MapFrom(src => true))
               .ForMember(
                   dest => dest.IsDeleted,
                   opt => opt.MapFrom(src => false))
               .ForMember(
                   dest => dest.TotalJob,
                   opt => opt.MapFrom(src => 0))
               .ForMember(
                   dest => dest.TotalPrice,
                   opt => opt.MapFrom(src => 0))
               .ForMember(
                   dest => dest.Rank,
                   opt => opt.MapFrom(src => 0));
        }
    }
}
