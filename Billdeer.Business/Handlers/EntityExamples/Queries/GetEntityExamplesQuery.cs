using AutoMapper;
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

            public async Task<IDataResult<IEnumerable<EntityExample>>> Handle(GetEntityExamplesQuery request, CancellationToken cancellationToken)
            {
                var entity = await _entityExampleRepository.GetListAsync();

                return new DataResult<IEnumerable<EntityExample>>(entity, ResultStatus.Success);
            }
        }
    }
}
