using Billdeer.Business.Constants;
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

namespace Billdeer.Business.Handlers.OperationClaims.Queries
{
    public class GetOperationClaimQuery : IRequest<IDataResult<OperationClaim>>
    {
        public int OperationClaimId { get; set; }

        public class GetOperationClaimQueryHandler : IRequestHandler<GetOperationClaimQuery, IDataResult<OperationClaim>>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;

            public GetOperationClaimQueryHandler(IOperationClaimRepository operationClaimRepository)
            {
                _operationClaimRepository = operationClaimRepository;
            }

            public async Task<IDataResult<OperationClaim>> Handle(GetOperationClaimQuery request, CancellationToken cancellationToken)
            {

                if (!IsClaimExists(request.OperationClaimId))
                {
                    return new DataResult<OperationClaim>(ResultStatus.Warning, Messages.NotFound);
                }

                var result = await _operationClaimRepository.GetAsync(o => o.Id == request.OperationClaimId);

                return new DataResult<OperationClaim>(result, ResultStatus.Success, Messages.NotFound);
            }

            private bool IsClaimExists(int claimId)
            {
                return _operationClaimRepository.Queryable(x => x.Id == claimId).Any();
            }
        }
    }
}
