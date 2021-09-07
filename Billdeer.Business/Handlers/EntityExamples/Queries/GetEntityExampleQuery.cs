using AutoMapper;
using Billdeer.Business.BusinessAspects.Autofac;
using Billdeer.Core.Aspects.Autofac.Caching;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.DataAccess.Abstract;
using Billdeer.Entities.Concrete;
using Billdeer.Entities.DTOs.EntityExampleDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.EntityExamples.Queries
{
    public class GetEntityExampleQuery : IRequest<IDataResult<EntityExampleDto>>
    {
        public int EntityExampleId { get; set; }
        public class GetEntityExampleQueryHandler : IRequestHandler<GetEntityExampleQuery, IDataResult<EntityExampleDto>>
        {
            private readonly IEntityExampleRepository _entityExampleRepository;
            private IMapper _mapper;

            public GetEntityExampleQueryHandler(IEntityExampleRepository entityExampleRepository, IMapper mapper)
            {
                _entityExampleRepository = entityExampleRepository;
                _mapper = mapper;
            }

            [SecuredOperation("master", Priority = 1)]
            [CacheAspect(60)]
            public async Task<IDataResult<EntityExampleDto>> Handle(GetEntityExampleQuery request, CancellationToken cancellationToken)
            {
                var entity = await _entityExampleRepository.GetAsync(ee => ee.Id == request.EntityExampleId);

                if (entity is null)
                    return new DataResult<EntityExampleDto>(ResultStatus.Warning);

                var entityDto = _mapper.Map<EntityExample, EntityExampleDto>(entity);

                return new DataResult<EntityExampleDto>(entityDto, ResultStatus.Success);
            }
        }
    }
}
