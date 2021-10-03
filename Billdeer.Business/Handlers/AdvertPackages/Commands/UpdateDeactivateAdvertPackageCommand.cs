using AutoMapper;
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

namespace Billdeer.Business.Handlers.AdvertPackages.Commands
{
    public class UpdateDeactivateAdvertPackageCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class UpdateDeactivateAdvertPackageCommandHandler : IRequestHandler<UpdateDeactivateAdvertPackageCommand, IResult>
        {
            private readonly IAdvertPackageRepository _advertPackageRepository;
            private readonly IMapper _mapper;

            public UpdateDeactivateAdvertPackageCommandHandler(IAdvertPackageRepository advertPackageRepository, IMapper mapper)
            {
                _advertPackageRepository = advertPackageRepository;
                _mapper = mapper;
            }
            public async Task<IResult> Handle(UpdateDeactivateAdvertPackageCommand request, CancellationToken cancellationToken)
            {
                if (!IfEngine.Engine(await CheckEntities<IAdvertPackageRepository, AdvertPackage>.Exist(_advertPackageRepository, request.Id)))
                {
                    return new Result(ResultStatus.Warning, Messages.NotFound);
                }

                var deactivatedAdvertPackage = await _advertPackageRepository.GetAsync(x => x.Id == request.Id);
                deactivatedAdvertPackage.ModifiedDate = DateTime.Now;
                deactivatedAdvertPackage.IsActive = false;

                _advertPackageRepository.Update(deactivatedAdvertPackage);
                await _advertPackageRepository.SaveChangesAsync();

                return new DataResult<AdvertPackage>(deactivatedAdvertPackage, ResultStatus.Success, Messages.Deactivated);
            }
        }
    }
}
