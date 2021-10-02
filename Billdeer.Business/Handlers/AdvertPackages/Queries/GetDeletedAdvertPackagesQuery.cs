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
    public class GetDeletedAdvertPackagesQuery : IRequest<IDataResult<IEnumerable<AdvertPackage>>>
    {
        public class GetDeletedAdvertPackagesQueryHandler : IRequestHandler<GetDeletedAdvertPackagesQuery, IDataResult<IEnumerable<AdvertPackage>>>
        {
            private readonly IAdvertPackageRepository _advertPackageRepository;

            public GetDeletedAdvertPackagesQueryHandler(IAdvertPackageRepository advertPackageRepository)
            {
                _advertPackageRepository = advertPackageRepository;
            }

            public async Task<IDataResult<IEnumerable<AdvertPackage>>> Handle(GetDeletedAdvertPackagesQuery request, CancellationToken cancellationToken)
            {
                var deletedAdvertPackages = await _advertPackageRepository.GetListAsync(x=> x.IsActive == false && x.IsDeleted == true);

                if (deletedAdvertPackages is null)
                {
                    return new DataResult<IEnumerable<AdvertPackage>>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<IEnumerable<AdvertPackage>>(deletedAdvertPackages, ResultStatus.Success, Messages.Success);
            }
        }
    }
}
