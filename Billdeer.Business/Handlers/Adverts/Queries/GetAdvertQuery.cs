using Billdeer.Business.Constants;
using Billdeer.Core.Aspects.Autofac.Caching;
using Billdeer.Core.Aspects.Autofac.Logging;
using Billdeer.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
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
    public class GetAdvertQuery : IRequest<IDataResult<Advert>>
    {
        public long Id { get; set; }

        public class GetAdvertQueryHandler : IRequestHandler<GetAdvertQuery, IDataResult<Advert>>
        {
            private readonly IAdvertRepository _advertRepository;

            public GetAdvertQueryHandler(IAdvertRepository advertRepository)
            {
                this._advertRepository = advertRepository;
            }

            [CacheAspect]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<Advert>> Handle(GetAdvertQuery request, CancellationToken cancellationToken)
            {
                if (!IfEngine.Engine(await CheckEntities<IAdvertRepository, Advert>.Exist(_advertRepository, request.Id)))
                {
                    return new DataResult<Advert>(ResultStatus.Warning, Messages.NotFound);
                }

                var advert = await _advertRepository.GetAsync(x => x.Id == request.Id && x.IsActive == true && x.IsDeleted == false);

                if(advert is null)
                {
                    return new DataResult<Advert>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<Advert>(advert, ResultStatus.Success, Messages.Success);

            }
        }
    }
}
