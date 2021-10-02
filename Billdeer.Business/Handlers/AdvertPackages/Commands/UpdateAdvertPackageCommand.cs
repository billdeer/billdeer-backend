using AutoMapper;
using Billdeer.Business.Constants;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.Core.Utilities.ToolKit;
using Billdeer.DataAccess.Abstract;
using Billdeer.Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.AdvertPackages.Commands
{
    public class UpdateAdvertPackageCommand : IRequest<IDataResult<AdvertPackage>>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DeliveryTime { get; set; }
        public int Revision { get; set; }
        public class UpdateAdvertPackageCommandHandler : IRequestHandler<UpdateAdvertPackageCommand, IDataResult<AdvertPackage>>
        {
            private readonly IAdvertPackageRepository _advertPackageRepository;
            private readonly IMapper _mapper;

            public UpdateAdvertPackageCommandHandler(IAdvertPackageRepository advertPackageRepository, IMapper mapper)
            {
                _advertPackageRepository = advertPackageRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<AdvertPackage>> Handle(UpdateAdvertPackageCommand request, CancellationToken cancellationToken)
            {
                if (!IfEngine.Engine(CheckEntities<IAdvertPackageRepository, AdvertPackage>.Exist(_advertPackageRepository, request.Id)))
                {
                    return new DataResult<AdvertPackage>(ResultStatus.Warning, Messages.NotFound);
                }

                var updatedAdvertPackage = await _advertPackageRepository.GetAsync(x => x.Id == request.Id && x.IsActive == true && x.IsDeleted == false);

                if (updatedAdvertPackage is null)
                {
                    return new DataResult<AdvertPackage>(ResultStatus.Warning, Messages.NotFound);
                }

                updatedAdvertPackage.ModifiedDate = DateTime.Now;
                updatedAdvertPackage.Name = request.Name;
                updatedAdvertPackage.Description = request.Description;
                updatedAdvertPackage.DeliveryTime = request.DeliveryTime;
                updatedAdvertPackage.Price = request.Price;
                updatedAdvertPackage.Revision = request.Revision;

                _advertPackageRepository.Update(updatedAdvertPackage);
                await _advertPackageRepository.SaveChangesAsync();

                return new DataResult<AdvertPackage>(updatedAdvertPackage, ResultStatus.Success, Messages.Updated);
            }
        }
    }
}
