using Billdeer.Business.Constants;
using Billdeer.Core.Entities.Concrete;
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
    public class GetDeactivatedFreelancerByUserIdQuery : IRequest<IDataResult<Freelancer>>
    {
        public long UserId { get; set; }

        public class GetDeactivatedFreelancerByUserIdQueryHandler : IRequestHandler<GetDeactivatedFreelancerByUserIdQuery, IDataResult<Freelancer>>
        {
            private readonly IFreelancerRepository _freelancerRepository;
            private readonly IUserRepository _userRepository;

            public GetDeactivatedFreelancerByUserIdQueryHandler(IFreelancerRepository freelancerRepository, IUserRepository userRepository)
            {
                _freelancerRepository = freelancerRepository;
                _userRepository = userRepository;
            }

            public async Task<IDataResult<Freelancer>> Handle(GetDeactivatedFreelancerByUserIdQuery request, CancellationToken cancellationToken)
            {
                if (!IfEngine.Engine(CheckEntities<IUserRepository, User>.Exist(_userRepository, request.UserId)))
                {
                    return new DataResult<Freelancer>(ResultStatus.Warning, Messages.UserNotFound);
                }

                var deactivatedfreelancerbyuserid = await _freelancerRepository.GetAsync(x => x.UserId == request.UserId && x.IsActive == false && x.IsDeleted == false);

                if (deactivatedfreelancerbyuserid is null)
                {
                    return new DataResult<Freelancer>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<Freelancer>(deactivatedfreelancerbyuserid, ResultStatus.Success, Messages.Success);
            }
        }
    }
}
