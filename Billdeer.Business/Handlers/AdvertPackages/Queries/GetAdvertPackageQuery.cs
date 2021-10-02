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
    public class GetAdvertPackageQuery : IRequest<IDataResult<AdvertPackage>>
    {
        public long Id { get; set; }

        public class GetAdvertPackageQueryHandler : IRequestHandler<GetAdvertPackageQuery, IDataResult<AdvertPackage>>
        {

            private readonly IAdvertPackageRepository _advertPackageRepository;

            public GetAdvertPackageQueryHandler(IAdvertPackageRepository advertPackageRepository)
            {
                _advertPackageRepository = advertPackageRepository;
            }

            public async Task<IDataResult<AdvertPackage>> Handle(GetAdvertPackageQuery request, CancellationToken cancellationToken)
            {
                if (!IfEngine.Engine(CheckEntities<IAdvertPackageRepository, AdvertPackage>.Exist(_advertPackageRepository, request.Id)))
                {
                    return new DataResult<AdvertPackage>(ResultStatus.Warning, Messages.NotFound);
                }

                var advertPackage = await _advertPackageRepository.GetAsync(x => x.Id == request.Id && x.IsActive == true && x.IsDeleted == false);

                if (advertPackage is null)
                {
                    return new DataResult<AdvertPackage>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<AdvertPackage>(advertPackage, ResultStatus.Success, Messages.Success);
            }
        }
    }
}
