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
    public class GetDeletedFreelancersQuery : IRequest<IDataResult<IEnumerable<Freelancer>>>
    {
        public class GetDeletedFreelancersQueryHandler : IRequestHandler<GetDeletedFreelancersQuery, IDataResult<IEnumerable<Freelancer>>>
        {
            private readonly IFreelancerRepository _freelancerRepository;

            public GetDeletedFreelancersQueryHandler(IFreelancerRepository freelancerRepository)
            {
                _freelancerRepository = freelancerRepository;
            }
            public async Task<IDataResult<IEnumerable<Freelancer>>> Handle(GetDeletedFreelancersQuery request, CancellationToken cancellationToken)
            {
                var freelancers = await _freelancerRepository.GetListAsync(x => x.IsActive == false && x.IsDeleted == true);

                if (freelancers is null)
                {
                    return new DataResult<IEnumerable<Freelancer>>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<IEnumerable<Freelancer>>(freelancers, ResultStatus.Success, Messages.Success);
            }
        }
    }
}
