using AutoMapper;
using Billdeer.Business.Constants;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.Core.Utilities.ToolKit;
using Billdeer.DataAccess.Abstract;
using Billdeer.Entities.Concrete;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.AdvertPackages.Commands
{
    public class FakeDeleteAdvertPackageCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class FakeDeleteAdvertPackageCommandHandler : IRequestHandler<FakeDeleteAdvertPackageCommand, IResult>
        {
            private readonly IAdvertPackageRepository _advertPackageRepository;
            private readonly IMapper _mapper;

            public FakeDeleteAdvertPackageCommandHandler(IAdvertPackageRepository advertPackageRepository, IMapper mapper)
            {
                _advertPackageRepository = advertPackageRepository;
                _mapper = mapper;
            }
            public async Task<IResult> Handle(FakeDeleteAdvertPackageCommand request, CancellationToken cancellationToken)
            {

                bool funcs = CheckEntities<IAdvertPackageRepository, AdvertPackage>.Exist(_advertPackageRepository, request.Id);
                if (!IfEngine.Engine(funcs))
                {
                    return new Result(ResultStatus.Warning, Messages.NotFound);
                }

                var advertPackage = await _advertPackageRepository.GetAsync(x => x.Id == request.Id);

                advertPackage.IsDeleted = true;
                advertPackage.DeletedDate = DateTime.Now;
                advertPackage.IsActive = false;

                _advertPackageRepository.Update(advertPackage);
                await _advertPackageRepository.SaveChangesAsync();

                return new Result(ResultStatus.Success, Messages.Deleted);
            }
        }
    }
}
