using AutoMapper;
using Billdeer.Business.Constants;
using Billdeer.Core.Aspects.Autofac.Caching;
using Billdeer.Core.Aspects.Autofac.Logging;
using Billdeer.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
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

namespace Billdeer.Business.Handlers.Adverts.Commands
{
    public class CreateAdvertCommand : IRequest<IDataResult<Advert>>
    {
        public long FreelancerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public class CreateAdvertCommandHandler : IRequestHandler<CreateAdvertCommand, IDataResult<Advert>>
        {
            private readonly IAdvertRepository _advertRepository;
            private readonly IFreelancerRepository _freelancerRepository;
            private readonly IMapper _mapper;

            public CreateAdvertCommandHandler(IAdvertRepository advertRepository, IFreelancerRepository freelancerRepository, IMapper mapper)
            {
                _advertRepository = advertRepository;
                _freelancerRepository = freelancerRepository;
                _mapper = mapper;
            }

            [RemoveCacheAspect("Get")]
            [LogAspect(typeof(PostgreSqlLogger))]
            public async Task<IDataResult<Advert>> Handle(CreateAdvertCommand request, CancellationToken cancellationToken)
            {
                bool funcs = CheckEntities<IFreelancerRepository, Freelancer>.Exist(_freelancerRepository, request.FreelancerId);
                if (!IfEngine.Engine(funcs))
                {
                    return new DataResult<Advert>(ResultStatus.Warning, Messages.NotFound);
                }

                var addedAdvert = _mapper.Map<Advert>(request);

                _advertRepository.Add(addedAdvert);
                await _advertRepository.SaveChangesAsync();

                return new DataResult<Advert>(addedAdvert, ResultStatus.Success, Messages.Added);
            }
        }
    }
}
