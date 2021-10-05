using Billdeer.Business.Constants;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.Core.Utilities.ToolKit;
using Billdeer.DataAccess.Abstract;
using Billdeer.Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.AdvertPackages.Queries
{
    public class GetDeactivatedAdvertPackagesByAdvertIdQuery : IRequest<IDataResult<IEnumerable<AdvertPackage>>>
    {
        public long AdvertId { get; set; }

        public class GetDeactivatedAdvertPackagesByAdvertIdQueryHandler : IRequestHandler<GetDeactivatedAdvertPackagesByAdvertIdQuery, IDataResult<IEnumerable<AdvertPackage>>>
        {
            private readonly IAdvertPackageRepository _advertPackageRepository;
            private readonly IAdvertRepository _advertRepository;

            public GetDeactivatedAdvertPackagesByAdvertIdQueryHandler(IAdvertPackageRepository advertPackageRepository, IAdvertRepository advertRepository)
            {
                _advertPackageRepository = advertPackageRepository;
                _advertRepository = advertRepository;
            }

            public async Task<IDataResult<IEnumerable<AdvertPackage>>> Handle(GetDeactivatedAdvertPackagesByAdvertIdQuery request, CancellationToken cancellationToken)
            {
                bool funcs = CheckEntities<IAdvertRepository, Advert>.Exist(_advertRepository, request.AdvertId);
                if (IfEngine.Engine(funcs))
                {
                    return new DataResult<IEnumerable<AdvertPackage>>(ResultStatus.Warning, Messages.NotFound);
                }

                var deactivatedAdvertPackages = await _advertPackageRepository.GetListAsync(x => x.AdvertId == request.AdvertId && x.IsActive == false && x.IsDeleted == false);

                if (deactivatedAdvertPackages is null)
                {
                    return new DataResult<IEnumerable<AdvertPackage>>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<IEnumerable<AdvertPackage>>(deactivatedAdvertPackages, ResultStatus.Success, Messages.Success);
            }
        }
    }
}
