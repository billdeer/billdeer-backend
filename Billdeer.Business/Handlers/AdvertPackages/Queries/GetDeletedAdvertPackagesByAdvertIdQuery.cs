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
    public class GetDeletedAdvertPackagesByAdvertIdQuery : IRequest<IDataResult<IEnumerable<AdvertPackage>>>
    {
        public long AdvertId { get; set; }

        public class GetDeletedAdvertPackagesByAdvertIdQueryHandler : IRequestHandler<GetDeletedAdvertPackagesByAdvertIdQuery, IDataResult<IEnumerable<AdvertPackage>>>
        {
            private readonly IAdvertPackageRepository _advertPackageRepository;
            private readonly IAdvertRepository _advertRepository;

            public GetDeletedAdvertPackagesByAdvertIdQueryHandler(IAdvertPackageRepository advertPackageRepository, IAdvertRepository advertRepository)
            {
                _advertPackageRepository = advertPackageRepository;
                _advertRepository = advertRepository;
            }

            public async Task<IDataResult<IEnumerable<AdvertPackage>>> Handle(GetDeletedAdvertPackagesByAdvertIdQuery request, CancellationToken cancellationToken)
            {
                bool funcs = CheckEntities<IAdvertRepository, Advert>.Exist(_advertRepository, request.AdvertId);
                if (IfEngine.Engine(funcs))
                {
                    return new DataResult<IEnumerable<AdvertPackage>>(ResultStatus.Warning, Messages.NotFound);
                }

                var deletedAdvertPackages = await _advertPackageRepository.GetListAsync(x => x.AdvertId == request.AdvertId && x.IsActive == false && x.IsDeleted == true);

                if (deletedAdvertPackages is null)
                {
                    return new DataResult<IEnumerable<AdvertPackage>>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<IEnumerable<AdvertPackage>>(deletedAdvertPackages, ResultStatus.Success, Messages.Success);
            }
        }
    }   
}
