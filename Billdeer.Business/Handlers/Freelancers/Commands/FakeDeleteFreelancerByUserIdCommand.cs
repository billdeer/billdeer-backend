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

namespace Billdeer.Business.Handlers.Freelancers.Commands
{
    public class FakeDeleteFreelancerByUserIdCommand : IRequest<IResult>
    {
        public long UserId { get; set; }

        public class FakeDeleteFreelancerByUserIdCommandHandler : IRequestHandler<FakeDeleteFreelancerByUserIdCommand, IResult>
        {
            private readonly IFreelancerRepository _freelancerRepository;
            private readonly IUserRepository _userRepository;

            public FakeDeleteFreelancerByUserIdCommandHandler(IFreelancerRepository freelancerRepository, IUserRepository userRepository)
            {
                _freelancerRepository = freelancerRepository;
                _userRepository = userRepository;
            }

            public async Task<IResult> Handle(FakeDeleteFreelancerByUserIdCommand request, CancellationToken cancellationToken)
            {
                if (!IfEngine.Engine(CheckEntities<IUserRepository, User>.Exist(_userRepository, request.UserId)))
                {
                    return new Result(ResultStatus.Warning, Messages.NotFound);
                }

                var freelancer = await _freelancerRepository.GetAsync(x => x.UserId == request.UserId);

                if (freelancer is null)
                {
                    return new Result(ResultStatus.Warning, Messages.NotFound);
                }

                freelancer.DeletedDate = DateTime.Now;
                freelancer.IsActive = false;
                freelancer.IsDeleted = true;

                _freelancerRepository.Update(freelancer);
                await _freelancerRepository.SaveChangesAsync();

                return new Result(ResultStatus.Success, Messages.Success);
            }
        }
    }
}
