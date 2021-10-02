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
    public class GetDeactivatedAdvertsQuery : IRequest<IDataResult<IEnumerable<Advert>>>
    {
        public class GetDeactivatedAdvertsQueryHandler : IRequestHandler<GetDeactivatedAdvertsQuery, IDataResult<IEnumerable<Advert>>>
        {
            private readonly IAdvertRepository _advertRepository;

            public GetDeactivatedAdvertsQueryHandler(IAdvertRepository advertRepository)
            {
                _advertRepository = advertRepository;
            }

            public async Task<IDataResult<IEnumerable<Advert>>> Handle(GetDeactivatedAdvertsQuery request, CancellationToken cancellationToken)
            {
                var deactivatedAdverts = await _advertRepository.GetListAsync(x => x.IsActive == false && x.IsDeleted == false);

                if (deactivatedAdverts is null)
                {
                    return new DataResult<IEnumerable<Advert>>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<IEnumerable<Advert>>(deactivatedAdverts, ResultStatus.Success, Messages.Success);
            }
        }
    }
}
