using AutoMapper;
using Billdeer.Business.Handlers.EntityExamples.Commands;
using Billdeer.Entities.Concrete;
using Billdeer.Entities.DTOs.EntityExampleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Business.AutoMapper.Profiles
{
    public class EntityExampleProfile : Profile 
    {
        public EntityExampleProfile()
        {
            CreateMap<CreateEntityExampleCommand, EntityExample>()
                .ForMember(
                    dest => dest.CreatedDate,  // dest: map ettiğimiz EntityExample'ı temsil ediyor.
                    opt => opt.MapFrom(src => DateTime.Now)) // src: gönderdiğimiz EntityExampleAddDto'yu temsil ediyor ama bi değerini kullanmak zorunda değiliz.
                .ForMember(
                    dest => dest.IsActive,
                    opt => opt.MapFrom(src => true)) // daha Map işlemi yapılırken otomatik olarak IsActive değerini true yapıyoruz.
                .ForMember(
                    dest => dest.IsDeleted,
                    opt => opt.MapFrom(src => false));

            // UpdateDto'dan Entity'e
            CreateMap<UpdateEntityExampleCommand, EntityExample>()
                .ForMember(
                    dest => dest.ModifiedDate,
                    opt => opt.MapFrom(src => DateTime.Now));

            // DeleteDto'dan Entity'e
            CreateMap<DeleteEntityExampleCommand, EntityExample>()
                .ForMember(
                    dest => dest.DeletedDate,
                    opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(
                    dest => dest.IsActive,
                    opt => opt.MapFrom(src => false))
                .ForMember(
                    dest => dest.IsDeleted,
                    opt => opt.MapFrom(src => true)); // burda da aynı Add mantığındaki gibi değerlerini değiştiriyoruz.

            CreateMap<EntityExample, EntityExampleDto>();


        }
    }
}
