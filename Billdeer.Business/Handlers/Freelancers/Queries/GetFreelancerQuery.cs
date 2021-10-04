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
    public class GetFreelancerQuery : IRequest<IDataResult<Freelancer>>
    {
        public long Id { get; set; }

        public class GetFreelancerQueryHandler : IRequestHandler<GetFreelancerQuery, IDataResult<Freelancer>>
        {

            private readonly IFreelancerRepository _freelancerRepository;

            public GetFreelancerQueryHandler(IFreelancerRepository freelancerRepository)
            {
                _freelancerRepository = freelancerRepository;
            }

            public async Task<IDataResult<Freelancer>> Handle(GetFreelancerQuery request, CancellationToken cancellationToken)
            {
                if (IfEngine.Engine(await CheckEntities<IFreelancerRepository, Freelancer>.Exist(_freelancerRepository, request.Id)))
                {
                    return new DataResult<Freelancer>(ResultStatus.Warning, Messages.NotFound);
                }

                var freelancer = await _freelancerRepository.GetAsync(x => x.Id == request.Id && x.IsActive == true && x.IsDeleted == false);

                if (freelancer is null)
                {
                    return new DataResult<Freelancer>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<Freelancer>(freelancer, ResultStatus.Success, Messages.Success);
            }
        }
    }
}
