using AutoMapper;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.EntityExamples.Commands
{
    public class DeleteEntityExampleCommand : IRequest<IResult>
    {
        public int EntityExampleId { get; set; }

        private class DeleteEntityExampleCommandHandler : IRequestHandler<DeleteEntityExampleCommand, IResult>
        {
            private readonly IEntityExampleRepository _entityExampleRepository;

            public DeleteEntityExampleCommandHandler(IEntityExampleRepository entityExampleRepository)
            {
                _entityExampleRepository = entityExampleRepository;
            }

            public async Task<IResult> Handle(DeleteEntityExampleCommand request, CancellationToken cancellationToken)
            {
                var entity = _entityExampleRepository.GetAsync(x => x.Id == request.EntityExampleId);

                if (entity is null)
                    new Result(ResultStatus.Warning);

                _entityExampleRepository.Delete(entity.Result);
                await _entityExampleRepository.SaveChangesAsync();

                return new Result(ResultStatus.Success);

            }
        }
    }
}
