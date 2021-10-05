using Billdeer.Business.Constants;
using Billdeer.Core.Aspects.Autofac.Caching;
using Billdeer.Core.Aspects.Autofac.Logging;
using Billdeer.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Billdeer.Core.Entities.Concrete;
using Billdeer.Core.Utilities.Mail;
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

namespace Billdeer.Business.Handlers.Adverts.Queries
{
    public class GetAdvertsByFreelancerIdQuery : IRequest<IDataResult<IEnumerable<Advert>>>
    {
        public long FreelancerId { get; set; }

        public class GetAdvertsByFreelancerIdQueryHandler : IRequestHandler<GetAdvertsByFreelancerIdQuery, IDataResult<IEnumerable<Advert>>>
        {
            private readonly IAdvertRepository _advertRepository;
            private readonly IFreelancerRepository _freelancerRepository;

            public GetAdvertsByFreelancerIdQueryHandler(IAdvertRepository advertRepository, IFreelancerRepository freelancerRepository)
            {
                _advertRepository = advertRepository;
                _freelancerRepository = freelancerRepository;
            }

            [CacheAspect]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Advert>>> Handle(GetAdvertsByFreelancerIdQuery request, CancellationToken cancellationToken)
            {
                var user = await _freelancerRepository.GetAsync(x => x.Id == request.FreelancerId);

                bool funcs = CheckEntities<IFreelancerRepository, Freelancer>.Exist(_freelancerRepository, request.FreelancerId);
                if (!IfEngine.Engine(funcs))
                {
                    return new DataResult<IEnumerable<Advert>>(ResultStatus.Warning, Messages.NotFound);
                }

                var advert = await _advertRepository.GetListAsync(x => x.FreelancerId == request.FreelancerId && x.IsActive == true && x.IsDeleted == false);

                if (advert is null)
                {
                    return new DataResult<IEnumerable<Advert>>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<IEnumerable<Advert>>(advert, ResultStatus.Success, Messages.Success);
            }
        }
    }
}
