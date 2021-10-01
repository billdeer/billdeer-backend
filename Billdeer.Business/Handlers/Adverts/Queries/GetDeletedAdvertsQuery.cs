using Billdeer.Business.Constants;
using Billdeer.Core.Aspects.Autofac.Caching;
using Billdeer.Core.Aspects.Autofac.Logging;
using Billdeer.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
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
    public class GetDeletedAdvertsQuery : IRequest<IDataResult<IEnumerable<Advert>>>
    {
        public class GetDeletedAdvertsQueryHandler : IRequestHandler<GetDeletedAdvertsQuery, IDataResult<IEnumerable<Advert>>>
        {
            private readonly IAdvertRepository _advertRepository;

            public GetDeletedAdvertsQueryHandler(IAdvertRepository advertRepository)
            {
                this._advertRepository = advertRepository;
            }

            [CacheAspect]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Advert>>> Handle(GetDeletedAdvertsQuery request, CancellationToken cancellationToken)
            {
                var advert = await _advertRepository.GetListAsync(x => x.IsActive == false && x.IsDeleted == true);

                if (advert is null)
                {
                    return new DataResult<IEnumerable<Advert>>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<IEnumerable<Advert>>(advert, ResultStatus.Success, Messages.Success);
            }

        }
    }
}
