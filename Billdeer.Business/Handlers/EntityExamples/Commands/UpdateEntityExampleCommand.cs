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

namespace Billdeer.Business.Handlers.EntityExamples.Commands
{
    public class UpdateEntityExampleCommand : IRequest<IDataResult<EntityExample>>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateEntityExampleCommandHandler : IRequestHandler<UpdateEntityExampleCommand, IDataResult<EntityExample>>
        {
            private readonly IEntityExampleRepository _entityExampleRepository;

            public UpdateEntityExampleCommandHandler(IEntityExampleRepository entityExampleRepository)
            {
                _entityExampleRepository = entityExampleRepository;
            }

            public async Task<IDataResult<EntityExample>> Handle(UpdateEntityExampleCommand request, CancellationToken cancellationToken)
            {
                var entityExample = await _entityExampleRepository.GetAsync(x => x.Id == request.Id);

                if (entityExample is null)
                {
                    return new DataResult<EntityExample>(ResultStatus.Warning);
                }

                var updatedEntityExample = new EntityExample
                {
                    Id = request.Id,
                    Name = request.Name
                };

                _entityExampleRepository.Update(updatedEntityExample);
                await _entityExampleRepository.SaveChangesAsync();

                return new DataResult<EntityExample>(updatedEntityExample, ResultStatus.Success);
            }
        }
    }
}
