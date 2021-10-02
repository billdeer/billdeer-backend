using Billdeer.Business.Constants;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.DataAccess.Abstract;
using Billdeer.Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.AdvertPackages.Queries
{
    public class GetDeactivatedAdvertPackagesQuery : IRequest<IDataResult<IEnumerable<AdvertPackage>>>
    {
        public class GetDeactivatedAdvertPackagesQueryHandler : IRequestHandler<GetDeactivatedAdvertPackagesQuery, IDataResult<IEnumerable<AdvertPackage>>>
        {
            private readonly IAdvertPackageRepository _advertPackageRepository;

            public GetDeactivatedAdvertPackagesQueryHandler(IAdvertPackageRepository advertPackageRepository)
            {
                _advertPackageRepository = advertPackageRepository;
            }

            public async Task<IDataResult<IEnumerable<AdvertPackage>>> Handle(GetDeactivatedAdvertPackagesQuery request, CancellationToken cancellationToken)
            {
                var deactivatedAdvertPackages = await _advertPackageRepository.GetListAsync(x => x.IsActive == false && x.IsDeleted == false);

                if (deactivatedAdvertPackages is null)
                {
                    return new DataResult<IEnumerable<AdvertPackage>>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<IEnumerable<AdvertPackage>>(deactivatedAdvertPackages, ResultStatus.Success, Messages.Success);
            }
        }
    }
}
