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
    public class EntityExampleProfile : Profile // AutoMapper paketinden geliyor
    {
        public EntityExampleProfile()
        {
            // AddDto'dan Entity'e

            // AutoMapper DTO ve Entity'de hem tip hem isim olarak birebir karşılık gelenleri otomatik mapliyor.
            // İki tarafta da "string Name" olarak belirttiğimiz için burda maplememize gerek kalmıyor.
            // Ama DTO'da CreatedDate olmadığı için burda bu şekilde mapliyoruz.
            CreateMap<EntityExampleAddDto, EntityExample>()
                .ForMember(
                    dest => dest.CreatedDate,  // dest: map ettiğimiz EntityExample'ı temsil ediyor.
                    opt => opt.MapFrom(src => DateTime.Now)) // src: gönderdiğimiz EntityExampleAddDto'yu temsil ediyor ama bi değerini kullanmak zorunda değiliz.
                .ForMember(
                    dest => dest.IsActive,
                    opt => opt.MapFrom(src => true)) // daha Map işlemi yapılırken otomatik olarak IsActive değerini true yapıyoruz.
                .ForMember(
                    dest => dest.IsDeleted,
                    opt => opt.MapFrom(src => false))
                .ReverseMap();

            // Command Mappings
            CreateMap<CreateEntityExampleCommand, EntityExample>()
                .ForMember(
                    dest => dest.CreatedDate,  // dest: map ettiğimiz EntityExample'ı temsil ediyor.
                    opt => opt.MapFrom(src => DateTime.Now)) // src: gönderdiğimiz EntityExampleAddDto'yu temsil ediyor ama bi değerini kullanmak zorunda değiliz.
                .ForMember(
                    dest => dest.IsActive,
                    opt => opt.MapFrom(src => true)) // daha Map işlemi yapılırken otomatik olarak IsActive değerini true yapıyoruz.
                .ForMember(
                    dest => dest.IsDeleted,
                    opt => opt.MapFrom(src => false))
                .ReverseMap();

            // UpdateDto'dan Entity'e
            CreateMap<EntityExampleUpdateDto, EntityExample>()
                .ForMember(
                    dest => dest.ModifiedDate,
                    opt => opt.MapFrom(src => DateTime.Now));

            // DeleteDto'dan Entity'e
            CreateMap<EntityExampleDeleteDto, EntityExample>()
                .ForMember(
                    dest => dest.DeletedDate,
                    opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(
                    dest => dest.IsActive,
                    opt => opt.MapFrom(src => false))
                .ForMember(
                    dest => dest.IsDeleted,
                    opt => opt.MapFrom(src => true)) // burda da aynı Add mantığındaki gibi değerlerini değiştiriyoruz.
                .ReverseMap(); 

            // Entity'den GetDto'ya. Veritabanından çektiğimiz Entity'i Dto'ya maplemek için kullanıcaz.
            CreateMap<EntityExample, EntityExampleDto>()
                .ForMember(
                    dest => dest.ForeignExamples, // EntityExampleDto'daki ICollection<ForeignExampleDto> listesine denk geliyor.
                    opt => opt.MapFrom(src => src.ForeignExamples)).ReverseMap(); // EntityExample'daki ICollection<ForeignExample> listesine denk geliyor.
            // Biri Dto listesi biri normal Entity listesi tuttuğu için tipleri farklı. O yüzden AutoMapper otomatik maplemiyor burda belirtmemiz gerekiyor.

            // iş kodlarını yazdıkça bunlar anlam kazanıcak kanka.



        }
    }
}
