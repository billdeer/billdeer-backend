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

namespace Billdeer.Business.Handlers.Adverts.Queries
{
    public class GetDeactivatedAdvertQuery : IRequest<IDataResult<Advert>>
    {
        public long Id { get; set; }

        public class GetDeactivatedAdvertQueryHandler : IRequestHandler<GetDeactivatedAdvertQuery, IDataResult<Advert>>
        {
            private readonly IAdvertRepository _advertRepository;

            public GetDeactivatedAdvertQueryHandler(IAdvertRepository advertRepository)
            {
                _advertRepository = advertRepository;
            }

            public async Task<IDataResult<Advert>> Handle(GetDeactivatedAdvertQuery request, CancellationToken cancellationToken)
            {
                bool funcs = CheckEntities<IAdvertRepository, Advert>.Exist(_advertRepository, request.Id);
                if (IfEngine.Engine(funcs))
                {
                    return new DataResult<Advert>(ResultStatus.Warning, Messages.NotFound);
                }

                var deactivatedAdvert = await _advertRepository.GetAsync(x => x.Id == request.Id && x.IsActive == false && x.IsDeleted == false);

                if (deactivatedAdvert is null)
                {
                    return new DataResult<Advert>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<Advert>(deactivatedAdvert, ResultStatus.Success, Messages.Success);
            }
        }
    }
}
