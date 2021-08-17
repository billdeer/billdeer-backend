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
    public class GetEntityExampleQuery : IRequest<IResult>
    {
        public int EntityExampleId { get; set; }
        public class GetEntityExampleQueryHandler : IRequestHandler<GetEntityExampleQuery, IResult>
        {
            private readonly IEntityExampleRepository _entityExampleRepository;
            private readonly IMapper _mapper;

            public GetEntityExampleQueryHandler(IEntityExampleRepository entityExampleRepository, IMapper mapper)
            {
                _entityExampleRepository = entityExampleRepository;
                _mapper = mapper;
            }

            public async Task<IResult> Handle(GetEntityExampleQuery request, CancellationToken cancellationToken)
            {
                var entity = await _entityExampleRepository.GetAsync(ee => ee.Id == request.EntityExampleId);

                if (entity is null)
                    return new Result(ResultStatus.Warning);

                var result = _mapper.Map<EntityExample, EntityExampleDto>(entity);

                return new DataResult<EntityExampleDto>(result, ResultStatus.Success);
            }
        }
    }
}
