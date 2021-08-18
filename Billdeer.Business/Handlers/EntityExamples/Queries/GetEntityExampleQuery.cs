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
    public class GetEntityExampleQuery : IRequest<IDataResult<EntityExample>>
    {
        public int EntityExampleId { get; set; }
        public class GetEntityExampleQueryHandler : IRequestHandler<GetEntityExampleQuery, IDataResult<EntityExample>>
        {
            private readonly IEntityExampleRepository _entityExampleRepository;

            public GetEntityExampleQueryHandler(IEntityExampleRepository entityExampleRepository)
            {
                _entityExampleRepository = entityExampleRepository;
            }

            public async Task<IDataResult<EntityExample>> Handle(GetEntityExampleQuery request, CancellationToken cancellationToken)
            {
                var entity = await _entityExampleRepository.GetAsync(ee => ee.Id == request.EntityExampleId);

                if (entity is null)
                    return new DataResult<EntityExample>(ResultStatus.Warning);

                return new DataResult<EntityExample>(entity, ResultStatus.Success);
            }
        }
    }
}
