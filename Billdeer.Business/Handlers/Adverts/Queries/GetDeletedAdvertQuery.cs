using Billdeer.Business.Constants;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.Core.Utilities.ToolKit;
using Billdeer.DataAccess.Abstract;
using Billdeer.Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.Adverts.Queries
{
    public class GetDeletedAdvertQuery : IRequest<IDataResult<Advert>>
    {
        public long Id { get; set; }

        public class GetAdvertQueryHandler : IRequestHandler<GetDeletedAdvertQuery, IDataResult<Advert>>
        {
            private readonly IAdvertRepository _advertRepository;

            public GetAdvertQueryHandler(IAdvertRepository advertRepository)
            {
                this._advertRepository = advertRepository;
            }

            public async Task<IDataResult<Advert>> Handle(GetDeletedAdvertQuery request, CancellationToken cancellationToken)
            {
                if (!IfEngine.Engine(CheckEntities<IAdvertRepository, Advert>.Exist(_advertRepository, request.Id)))
                {
                    return new DataResult<Advert>(ResultStatus.Warning, Messages.NotFound);
                }

                var advert = await _advertRepository.GetAsync(x => x.Id == request.Id && x.IsActive == false && x.IsDeleted == true);

                if(advert is null)
                {
                    return new DataResult<Advert>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<Advert>(advert, ResultStatus.Success, Messages.Success);

            }
        }
    }
}
