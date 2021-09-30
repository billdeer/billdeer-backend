using Billdeer.Business.Constants;
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
    public class GetAdvertsQuery : IRequest<IDataResult<IEnumerable<Advert>>>
    {
        public class GetAdvertsQueryHandler : IRequestHandler<GetAdvertsQuery, IDataResult<IEnumerable<Advert>>>
        {
            private readonly IAdvertRepository _advertRepository;

            public GetAdvertsQueryHandler(IAdvertRepository advertRepository)
            {
                this._advertRepository = advertRepository;
            }

            public async Task<IDataResult<IEnumerable<Advert>>> Handle(GetAdvertsQuery request, CancellationToken cancellationToken)
            {
                var advert = await _advertRepository.GetListAsync(x => x.IsActive == true && x.IsDeleted == false);

                if (advert is null)
                {
                    return new DataResult<IEnumerable<Advert>>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<IEnumerable<Advert>>(advert, ResultStatus.Success, Messages.Success);
            }
        }
    }
}
