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

namespace Billdeer.Business.Handlers.AdvertPackages.Queries
{
    public class GetAdvertPackagesQuery : IRequest<IDataResult<IEnumerable<AdvertPackage>>>
    {
        public class GetAdvertPackagesQueryHandler : IRequestHandler<GetAdvertPackagesQuery, IDataResult<IEnumerable<AdvertPackage>>>
        {
            private readonly IAdvertPackageRepository _advertPackageRepository;

            public GetAdvertPackagesQueryHandler(IAdvertPackageRepository advertPackageRepository)
            {
                _advertPackageRepository = advertPackageRepository;
            }

            public async Task<IDataResult<IEnumerable<AdvertPackage>>> Handle(GetAdvertPackagesQuery request, CancellationToken cancellationToken)
            {
                var advertPackages = await _advertPackageRepository.GetListAsync(x => x.IsActive == true && x.IsDeleted == false);

                if (advertPackages is null)
                {
                    return new DataResult<IEnumerable<AdvertPackage>>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<IEnumerable<AdvertPackage>>(advertPackages, ResultStatus.Success, Messages.Success);
            }
        }
    }
}
