using AutoMapper;
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
    public class CreateEntityExampleCommandHandler : IRequestHandler<CreateEntityExampleCommand, IDataResult<EntityExampleDto>>
    {
        IEntityExampleRepository _entityExampleRepository;
        private readonly IMapper _mapper;

        public CreateEntityExampleCommandHandler(IEntityExampleRepository entityExampleRepository, IMapper mapper)
        {
            _entityExampleRepository = entityExampleRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<EntityExampleDto>> Handle(CreateEntityExampleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _entityExampleRepository.GetAsync(x => x.Name == request.Name);

            if (entity is not null)
                return new DataResult<EntityExampleDto>(ResultStatus.Warning);

            var entityMap = _mapper.Map<CreateEntityExampleCommand, EntityExample>(request);

            var entityExample = _entityExampleRepository.Add(entityMap);
            await _entityExampleRepository.SaveChangesAsync();

            var entityExampleDto = _mapper.Map<EntityExampleDto>(entityExample);

            return new DataResult<EntityExampleDto>(entityExampleDto, ResultStatus.Success);
        }
    }
}
