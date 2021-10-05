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
    public class GetFreelancerByUserIdQuery : IRequest<IDataResult<Freelancer>>
    {
        public long UserId { get; set; }

        public class GetFreelancerByUserIdQueryHandler : IRequestHandler<GetFreelancerByUserIdQuery, IDataResult<Freelancer>>
        {
            private readonly IFreelancerRepository _freelancerRepository;
            private readonly IUserRepository _userRepository;

            public GetFreelancerByUserIdQueryHandler(IFreelancerRepository freelancerRepository, IUserRepository userRepository)
            {
                _freelancerRepository = freelancerRepository;
                _userRepository = userRepository;
            }

            public async Task<IDataResult<Freelancer>> Handle(GetFreelancerByUserIdQuery request, CancellationToken cancellationToken)
            {
                if (IfEngine.Engine(CheckEntities<IUserRepository, User>.Exist(_userRepository, request.UserId)))
                {
                    return new DataResult<Freelancer>(ResultStatus.Warning, Messages.UserNotFound);
                }

                var freelancer = await _freelancerRepository.GetAsync(x => x.UserId == request.UserId && x.IsActive == true && x.IsDeleted == false);

                if (freelancer is null)
                { 
                    return new DataResult<Freelancer>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<Freelancer>(freelancer, ResultStatus.Success, Messages.Success);
            }
        }
    }
}
