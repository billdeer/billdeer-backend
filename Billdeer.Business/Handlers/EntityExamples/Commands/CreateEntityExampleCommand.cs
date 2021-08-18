using AutoMapper;
using Billdeer.Business.AutoMapper.Profiles;
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

namespace Billdeer.Business.Handlers.EntityExamples.Commands
{
    public class CreateEntityExampleCommand : IRequest<IDataResult<EntityExample>>
    {
        public string Name { get; set; }
            
        public class CreateEntityExampleCommandHandler : IRequestHandler<CreateEntityExampleCommand, IDataResult<EntityExample>>
        {
            IEntityExampleRepository _entityExampleRepository;

            public CreateEntityExampleCommandHandler(IEntityExampleRepository entityExampleRepository)
            {
                _entityExampleRepository = entityExampleRepository;
            }

            public async Task<IDataResult<EntityExample>> Handle(CreateEntityExampleCommand request, CancellationToken cancellationToken)
            {
                var entity = await _entityExampleRepository.GetAsync(x => x.Name == request.Name);

                if (entity is not null)
                    return new DataResult<EntityExample>(ResultStatus.Warning);

                var entityR = new EntityExample
                {
                    Name = request.Name,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    IsActive = true
                };

                var entityExample = _entityExampleRepository.Add(entityR);
                await _entityExampleRepository.SaveChangesAsync();

                var entityExampleDto = new EntityExampleDto
                {

                };

                return new DataResult<EntityExample>(entityExample, ResultStatus.Success);
            }
        }
        
    }
}
