using Billdeer.Business.Constants;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.DataAccess.Abstract;
using Billdeer.Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.Freelancers.Queries
{
    public class GetDeactivatedFreelancersQuery : IRequest<IDataResult<IEnumerable<Freelancer>>>
    {
        public class GetDeactivatedFreelancersQueryHandler : IRequestHandler<GetDeactivatedFreelancersQuery, IDataResult<IEnumerable<Freelancer>>>
        {
            private readonly IFreelancerRepository _freelancerRepository;

            public GetDeactivatedFreelancersQueryHandler(IFreelancerRepository freelancerRepository)
            {
                _freelancerRepository = freelancerRepository;
            }

            public async Task<IDataResult<IEnumerable<Freelancer>>> Handle(GetDeactivatedFreelancersQuery request, CancellationToken cancellationToken)
            {
                var deactivatedfreelancers = await _freelancerRepository.GetListAsync(x => x.IsActive == false && x.IsDeleted == false);

                if (deactivatedfreelancers is null)
                {
                    return new DataResult<IEnumerable<Freelancer>>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<IEnumerable<Freelancer>>(deactivatedfreelancers, ResultStatus.Success, Messages.Success);
            }
        }
    }
}
