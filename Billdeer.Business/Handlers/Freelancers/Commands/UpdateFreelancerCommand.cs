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
    public class UpdateFreelancerCommand : IRequest<IDataResult<Freelancer>>
    {
        public long Id { get; set; }
        public long TotalJob { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Rank { get; set; }

        public class UpdateFreelancerCommandHandler : IRequestHandler<UpdateFreelancerCommand, IDataResult<Freelancer>>
        {
            private readonly IFreelancerRepository _freelancerRepository;

            public UpdateFreelancerCommandHandler(IFreelancerRepository freelancerRepository)
            {
                _freelancerRepository = freelancerRepository;
            }

            public async Task<IDataResult<Freelancer>> Handle(UpdateFreelancerCommand request, CancellationToken cancellationToken)
            {
                if (!IfEngine.Engine(await CheckEntities<IFreelancerRepository, Freelancer>.Exist(_freelancerRepository, request.Id)))
                {
                    return new DataResult<Freelancer>(ResultStatus.Warning, Messages.NotFound);
                }

                var freelancer = await _freelancerRepository.GetAsync(x => x.Id == request.Id && x.IsActive == true && x.IsDeleted == false);

                if (freelancer is null)
                {
                    return new DataResult<Freelancer>(ResultStatus.Warning, Messages.NotFound);
                }

                freelancer.TotalJob = request.TotalJob;
                freelancer.TotalPrice = request.TotalPrice;
                freelancer.Rank = request.Rank;
                freelancer.ModifiedDate = DateTime.Now;

                _freelancerRepository.Update(freelancer);
                await _freelancerRepository.SaveChangesAsync();

                return new DataResult<Freelancer>(freelancer, ResultStatus.Success, Messages.Success);
            }
        }
    }
}
