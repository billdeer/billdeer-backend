using AutoMapper;
using Billdeer.Business.BusinessAspects.Autofac;
using Billdeer.Business.Constants;
using Billdeer.Business.Handlers.EntityExamples.ValidationRules.FluentValidation;
using Billdeer.Core.Aspects.Autofac.Caching;
using Billdeer.Core.Aspects.Autofac.Logging;
using Billdeer.Core.Aspects.Autofac.Validation;
using Billdeer.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.DataAccess.Abstract;
using Billdeer.Entities.Concrete;
using Billdeer.Entities.DTOs.EntityExampleDtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.EntityExamples.Commands
{
    public class CreateEntityExampleCommand : IRequest<IDataResult<EntityExampleDto>>
    {
        public string Name { get; set; }
            
        public class CreateEntityExampleCommandHandler : IRequestHandler<CreateEntityExampleCommand, IDataResult<EntityExampleDto>>
        {
            IEntityExampleRepository _entityExampleRepository;
            private readonly IMapper _mapper;

            public CreateEntityExampleCommandHandler(IEntityExampleRepository entityExampleRepository, IMapper mapper)
            {
                _entityExampleRepository = entityExampleRepository;
                this._mapper = mapper;
            }

            [ValidationAspect(typeof(CreateEntityExampleValidator), Priority = 1)]
            [RemoveCacheAspect("Get")]
            //[SecuredOperation("master", Priority = 1)]
            [LogAspect(typeof(PostgreSqlLogger))]
            public async Task<IDataResult<EntityExampleDto>> Handle(CreateEntityExampleCommand request, CancellationToken cancellationToken)
            {
                var entity = await _entityExampleRepository.GetAsync(x => x.Name == request.Name);

                if (entity is not null)
                    return new DataResult<EntityExampleDto>(ResultStatus.Warning, Messages.NotFound);

                var entityForDb = _mapper.Map<EntityExample>(request);

                var entityAdded = _entityExampleRepository.Add(entityForDb);
                await _entityExampleRepository.SaveChangesAsync();

                var entityResponseDto = _mapper.Map<EntityExampleDto>(entityAdded);

                return new DataResult<EntityExampleDto>(entityResponseDto, ResultStatus.Success);
            }
        }
        
    }
}
