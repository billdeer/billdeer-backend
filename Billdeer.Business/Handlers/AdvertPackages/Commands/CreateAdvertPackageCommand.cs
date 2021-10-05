using AutoMapper;
using Billdeer.Business.Constants;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.Core.Utilities.ToolKit;
using Billdeer.DataAccess.Abstract;
using Billdeer.Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.AdvertPackages.Commands
{
    public class CreateAdvertPackageCommand : IRequest<IDataResult<AdvertPackage>>
    {
        public long AdvertId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DeliveryTime { get; set; }
        public int Revision { get; set; }

        public class CreateAdvertPackageCommandHandler : IRequestHandler<CreateAdvertPackageCommand, IDataResult<AdvertPackage>>
        {
            private readonly IAdvertPackageRepository _advertPackageRepository;
            private readonly IAdvertRepository _advertRepository;
            private readonly IMapper _mapper;

            public CreateAdvertPackageCommandHandler(IAdvertPackageRepository advertPackageRepository, IAdvertRepository advertRepository, IMapper mapper)
            {
                _advertPackageRepository = advertPackageRepository;
                _advertRepository = advertRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<AdvertPackage>> Handle(CreateAdvertPackageCommand request, CancellationToken cancellationToken)
            {
                bool funcs = CheckEntities<IAdvertRepository, Advert>.Exist(_advertRepository, request.AdvertId);
                if (!IfEngine.Engine(funcs))
                {
                    return new DataResult<AdvertPackage>(ResultStatus.Warning, Messages.NotFound);
                }

                var addedAdvertPackage = _mapper.Map<AdvertPackage>(request);

                _advertPackageRepository.Add(addedAdvertPackage);
                await _advertPackageRepository.SaveChangesAsync();

                return new DataResult<AdvertPackage>(addedAdvertPackage, ResultStatus.Success, Messages.Added);
            }
        }

    }
}
