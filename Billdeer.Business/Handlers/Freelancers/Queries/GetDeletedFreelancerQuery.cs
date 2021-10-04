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
    public class GetDeletedFreelancerQuery : IRequest<IDataResult<Freelancer>>
    {
        public long Id { get; set; }

        public class GetDeletedFreelancerQueryHandler : IRequestHandler<GetDeletedFreelancerQuery, IDataResult<Freelancer>>
        {

            private readonly IFreelancerRepository _freelancerRepository;

            public GetDeletedFreelancerQueryHandler(IFreelancerRepository freelancerRepository)
            {
                _freelancerRepository = freelancerRepository;
            }

            public async Task<IDataResult<Freelancer>> Handle(GetDeletedFreelancerQuery request, CancellationToken cancellationToken)
            {
                if (IfEngine.Engine(await CheckEntities<IFreelancerRepository, Freelancer>.Exist(_freelancerRepository, request.Id)))
                {
                    return new DataResult<Freelancer>(ResultStatus.Warning, Messages.NotFound);
                }

                var freelancer = await _freelancerRepository.GetAsync(x => x.Id == request.Id && x.IsActive == false && x.IsDeleted == true);

                if (freelancer is null)
                {
                    return new DataResult<Freelancer>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<Freelancer>(freelancer, ResultStatus.Success, Messages.Success);
            }
        }
    }
}
