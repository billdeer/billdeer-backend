using AutoMapper;
using Billdeer.Business.Handlers.OperationClaims.Commands;
using Billdeer.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Business.AutoMapper.Profiles
{
    public class OperationClaimProfile : Profile
    {
        public OperationClaimProfile()
        {
            CreateMap<CreateOperationClaimCommand, OperationClaim>()
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
