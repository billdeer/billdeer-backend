using Billdeer.Business.Constants;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.Core.Utilities.ToolKit;
using Billdeer.DataAccess.Abstract;
using Billdeer.Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.AdvertPackages.Queries
{
    public class GetDeactivatedAdvertPackageQuery : IRequest<IDataResult<AdvertPackage>>
    {
        public long Id { get; set; }

        public class GetDeactivatedAdvertPackageQueryHandler : IRequestHandler<GetDeactivatedAdvertPackageQuery, IDataResult<AdvertPackage>>
        {
            private readonly IAdvertPackageRepository _advertPackageRepository;

            public GetDeactivatedAdvertPackageQueryHandler(IAdvertPackageRepository advertPackageRepository)
            {
                _advertPackageRepository = advertPackageRepository;
            }

            public async Task<IDataResult<AdvertPackage>> Handle(GetDeactivatedAdvertPackageQuery request, CancellationToken cancellationToken)
            {
                if (IfEngine.Engine(await CheckEntities<IAdvertPackageRepository, AdvertPackage>.Exist(_advertPackageRepository, request.Id)))
                {
                    return new DataResult<AdvertPackage>(ResultStatus.Warning, Messages.NotFound);
                }

                var deactivatedAdvertPackage = await _advertPackageRepository.GetAsync(x => x.Id == request.Id && x.IsActive == false && x.IsDeleted == false);

                if (deactivatedAdvertPackage is null)
                {
                    return new DataResult<AdvertPackage>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<AdvertPackage>(deactivatedAdvertPackage, ResultStatus.Success, Messages.Success);
            }
        }
    }
}
