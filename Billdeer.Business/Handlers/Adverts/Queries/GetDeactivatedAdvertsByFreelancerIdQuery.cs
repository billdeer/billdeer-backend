using Billdeer.Business.Constants;
using Billdeer.Core.Entities.Concrete;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.Core.Utilities.ToolKit;
using Billdeer.DataAccess.Abstract;
using Billdeer.Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.Adverts.Queries
{
    public class GetDeactivatedAdvertsByFreelancerIdQuery : IRequest<IDataResult<IEnumerable<Advert>>>
    {
        public long FreelancerId { get; set; }

        public class GetDeactivatedAdvertsByFreelancerIdQueryHandler : IRequestHandler<GetDeactivatedAdvertsByFreelancerIdQuery, IDataResult<IEnumerable<Advert>>>
        {
            private readonly IAdvertRepository _advertRepository;
            private readonly IFreelancerRepository _freelancerRepository;

            public GetDeactivatedAdvertsByFreelancerIdQueryHandler(IAdvertRepository advertRepository, IFreelancerRepository freelancerRepository)
            {
                _advertRepository = advertRepository;
                _freelancerRepository = freelancerRepository;
            }

            public async Task<IDataResult<IEnumerable<Advert>>> Handle(GetDeactivatedAdvertsByFreelancerIdQuery request, CancellationToken cancellationToken)
            {
                if (IfEngine.Engine(await CheckEntities<IFreelancerRepository, Freelancer>.Exist(_freelancerRepository, request.FreelancerId)))
                {
                    return new DataResult<IEnumerable<Advert>>(ResultStatus.Warning, Messages.NotFound);
                }

                var deactivatedAdverts = await _advertRepository.GetListAsync(x => x.FreelancerId == request.FreelancerId && x.IsActive == false && x.IsDeleted == false);

                if (deactivatedAdverts is null)
                {
                    return new DataResult<IEnumerable<Advert>>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<IEnumerable<Advert>>(deactivatedAdverts, ResultStatus.Success, Messages.Success);
            }
        }
    }
}
