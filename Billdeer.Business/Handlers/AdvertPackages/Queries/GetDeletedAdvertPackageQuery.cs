using Billdeer.Business.Constants;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.Core.Utilities.ToolKit;
using Billdeer.DataAccess.Abstract;
using Billdeer.Entities.Concrete;
using MediatR;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.AdvertPackages.Queries
{
    public class GetDeletedAdvertPackageQuery : IRequest<IDataResult<AdvertPackage>>
    {
        public long Id { get; set; }

        public class GetDeletedAdvertPackageQueryHandler : IRequestHandler<GetDeletedAdvertPackageQuery, IDataResult<AdvertPackage>>
        {
            private readonly IAdvertPackageRepository _advertPackageRepository;

            public GetDeletedAdvertPackageQueryHandler(IAdvertPackageRepository advertPackageRepository)
            {
                _advertPackageRepository = advertPackageRepository;
            }

            public async Task<IDataResult<AdvertPackage>> Handle(GetDeletedAdvertPackageQuery request, CancellationToken cancellationToken)
            {
                if (IfEngine.Engine(CheckEntities<IAdvertPackageRepository, AdvertPackage>.Exist(_advertPackageRepository, request.Id)))
                {
                    return new DataResult<AdvertPackage>(ResultStatus.Warning, Messages.NotFound);
                }

                var deletedAdvertPackage = await _advertPackageRepository.GetAsync(x => x.Id == request.Id && x.IsActive == false && x.IsDeleted == true);

                if (deletedAdvertPackage is null)
                {
                    return new DataResult<AdvertPackage>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<AdvertPackage>(deletedAdvertPackage, ResultStatus.Success, Messages.Success);
            }
        }
    } 
}
