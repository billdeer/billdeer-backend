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

namespace Billdeer.Business.Handlers.Freelancers.Commands
{
    public class FakeDeleteFreelancerCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class FakeDeleteFreelancerCommandHandler : IRequestHandler<FakeDeleteFreelancerCommand, IResult>
        {
            private readonly IFreelancerRepository _freelancerRepository;

            public FakeDeleteFreelancerCommandHandler(IFreelancerRepository freelancerRepository)
            {
                _freelancerRepository = freelancerRepository;
            }

            public async Task<IResult> Handle(FakeDeleteFreelancerCommand request, CancellationToken cancellationToken)
            {
                if (!IfEngine.Engine(await CheckEntities<IFreelancerRepository, Freelancer>.Exist(_freelancerRepository, request.Id)))
                {
                    return new Result(ResultStatus.Warning, Messages.NotFound);
                }

                var freelancer = await _freelancerRepository.GetAsync(x => x.Id == request.Id && x.IsActive == true && x.IsDeleted == false);

                if (freelancer is null)
                {
                    return new Result(ResultStatus.Warning, Messages.NotFound);
                }

                freelancer.DeletedDate = DateTime.Now;
                freelancer.IsActive = false;
                freelancer.IsDeleted = true;

                return new Result(ResultStatus.Success, Messages.Success);
            }
        }
    }
}
