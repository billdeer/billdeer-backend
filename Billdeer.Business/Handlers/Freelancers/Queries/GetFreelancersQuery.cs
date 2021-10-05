using Billdeer.Business.Constants;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.Core.Utilities.ToolKit;
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
    public class GetFreelancersQuery : IRequest<IDataResult<IEnumerable<Freelancer>>>
    {

        public class GetFreelancersQueryHandler : IRequestHandler<GetFreelancersQuery, IDataResult<IEnumerable<Freelancer>>>
        {
            private readonly IFreelancerRepository _freelancerRepository;

            public GetFreelancersQueryHandler(IFreelancerRepository freelancerRepository)
            {
                _freelancerRepository = freelancerRepository;
            }

            public async Task<IDataResult<IEnumerable<Freelancer>>> Handle(GetFreelancersQuery request, CancellationToken cancellationToken)
            {
                var freelancers = await _freelancerRepository.GetListAsync(x => x.IsActive == true && x.IsDeleted == false);

                if (freelancers is null)
                {
                    return new DataResult<IEnumerable<Freelancer>>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<IEnumerable<Freelancer>>(freelancers, ResultStatus.Success, Messages.Success);
            }
        }
    }
}
