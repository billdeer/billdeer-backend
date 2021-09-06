using AutoMapper;
using Billdeer.Business.Constants;
using Billdeer.Business.Handlers.OperationClaims.ValidationRules;
using Billdeer.Core.Aspects.Autofac.Validation;
using Billdeer.Core.Entities.Concrete;
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

namespace Billdeer.Business.Handlers.OperationClaims.Commands
{
    public class CreateOperationClaimCommand : IRequest<IDataResult<OperationClaim>>
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }

        public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, IDataResult<OperationClaim>>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;

            public CreateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
            }

            [ValidationAspect(typeof(CreateOperationClaimValidator), Priority = 1)]
            public async Task<IDataResult<OperationClaim>> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                var operationClaimExist = await _operationClaimRepository.GetAsync(o => o.Name == request.Name && o.Alias == request.Alias);

                if (operationClaimExist is not null)
                    return new DataResult<OperationClaim>(operationClaimExist, ResultStatus.Warning, Messages.NameAlreadyExist);

                var operationClaimForAdd = _mapper.Map<OperationClaim>(request);

                _operationClaimRepository.Add(operationClaimForAdd);
                await _operationClaimRepository.SaveChangesAsync();

                return new DataResult<OperationClaim>(operationClaimForAdd, ResultStatus.Success, Messages.Added);
            }
        }
    }
}
