using AutoMapper;
using Billdeer.Business.BusinessAspects.Autofac;
using Billdeer.Core.Aspects.Autofac.Caching;
using Billdeer.Core.Aspects.Autofac.Logging;
using Billdeer.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.DataAccess.Abstract;
using Billdeer.Entities.Concrete;
using Billdeer.Entities.DTOs.EntityExampleDtos;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.EntityExamples.Queries
{
    public class GetEntityExamplesQuery : IRequest<IDataResult<IEnumerable<EntityExample>>>
    {
        public class GetEntityExamplesQueryHandler : IRequestHandler<GetEntityExamplesQuery, IDataResult<IEnumerable<EntityExample>>>
        {
            private readonly IEntityExampleRepository _entityExampleRepository;
            private IMapper _mapper;

            public GetEntityExamplesQueryHandler(IEntityExampleRepository entityExampleRepository, IMapper mapper)
            {
                _entityExampleRepository = entityExampleRepository;
                _mapper = mapper;
            }

            [SecuredOperation("master", Priority = 1)]
            [CacheAspect(60)]
            [LogAspect(typeof(PostgreSqlLogger))]
            public async Task<IDataResult<IEnumerable<EntityExample>>> Handle(GetEntityExamplesQuery request, CancellationToken cancellationToken)
            {
                var entity = await _entityExampleRepository.GetListAsync();

                return new DataResult<IEnumerable<EntityExample>>(entity, ResultStatus.Success);
            }
        }
    }
}
