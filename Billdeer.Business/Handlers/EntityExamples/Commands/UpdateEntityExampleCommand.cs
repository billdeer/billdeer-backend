using AutoMapper;
using Billdeer.Business.Constants;
using Billdeer.Core.Aspects.Autofac.Logging;
using Billdeer.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.Core.Utilities.ToolKit;
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

namespace Billdeer.Business.Handlers.EntityExamples.Commands
{
    public class UpdateEntityExampleCommand : IRequest<IDataResult<EntityExampleDto>>
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public class UpdateEntityExampleCommandHandler : IRequestHandler<UpdateEntityExampleCommand, IDataResult<EntityExampleDto>>
        {
            private readonly IEntityExampleRepository _entityExampleRepository;
            private readonly IMapper _mapper;

            public UpdateEntityExampleCommandHandler(IEntityExampleRepository entityExampleRepository, IMapper mapper)
            {
                _entityExampleRepository = entityExampleRepository;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<EntityExampleDto>> Handle(UpdateEntityExampleCommand request, CancellationToken cancellationToken)
            {
                var entityExample = await _entityExampleRepository.GetAsync(x => x.Id == request.Id);

                if (IfEngine.Engine(await CheckEntities<IEntityExampleRepository, EntityExample>.Exist(_entityExampleRepository, request.Id)))
                {
                    return new DataResult<EntityExampleDto>(ResultStatus.Warning, Messages.NotFound);
                }

                var updatedEntityExample = _mapper.Map<UpdateEntityExampleCommand, EntityExample>(request, entityExample);

                var entityEntityResponse = _entityExampleRepository.Update(updatedEntityExample);
                await _entityExampleRepository.SaveChangesAsync();

                var entityExampleDto = _mapper.Map<EntityExampleDto>(entityEntityResponse);

                return new DataResult<EntityExampleDto>(entityExampleDto, ResultStatus.Success);
            }
        }
    }
}
