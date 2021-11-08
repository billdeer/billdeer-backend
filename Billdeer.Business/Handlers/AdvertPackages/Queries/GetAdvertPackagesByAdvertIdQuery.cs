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

namespace Billdeer.Business.Handlers.AdvertPackages.Queries
{
    public class GetAdvertPackagesByAdvertIdQuery : IRequest<IDataResult<IEnumerable<AdvertPackage>>>
    {
        public long AdvertId { get; set; }

        public class GetAdvertPackagesByAdvertIdQueryHandler : IRequestHandler<GetAdvertPackagesByAdvertIdQuery, IDataResult<IEnumerable<AdvertPackage>>>
        {
            private readonly IAdvertPackageRepository _advertPackageRepository;
            private readonly IAdvertRepository _advertRepository;

            public GetAdvertPackagesByAdvertIdQueryHandler(IAdvertPackageRepository advertPackageRepository, IAdvertRepository advertRepository)
            {
                _advertPackageRepository = advertPackageRepository;
                _advertRepository = advertRepository;
            }

            public async Task<IDataResult<IEnumerable<AdvertPackage>>> Handle(GetAdvertPackagesByAdvertIdQuery request, CancellationToken cancellationToken)
            {
                if (!IfEngine.Engine(CheckEntities<IAdvertRepository, Advert>.Exist(_advertRepository, request.AdvertId)))
                {
                    return new DataResult<IEnumerable<AdvertPackage>>(ResultStatus.Warning, Messages.NotFound);
                }

                var advertPackages = await _advertPackageRepository.GetListAsync(x => x.AdvertId == request.AdvertId && x.IsActive == true && x.IsDeleted == false);

                if (advertPackages is null)
                {
                    return new DataResult<IEnumerable<AdvertPackage>>(ResultStatus.Warning, Messages.NotFound);
                }


                return new DataResult<IEnumerable<AdvertPackage>>(advertPackages, ResultStatus.Success, Messages.Success);
            }
        }
    }
}
