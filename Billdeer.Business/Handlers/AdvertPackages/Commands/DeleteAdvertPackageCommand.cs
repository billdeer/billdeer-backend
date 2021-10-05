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
    public class DeleteAdvertPackageCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class DeleteAdvertPackageCommandHandler : IRequestHandler<DeleteAdvertPackageCommand, IResult>
        {
            private readonly IAdvertPackageRepository _advertPackageRepository;
            private readonly IMapper _mapper;

            public DeleteAdvertPackageCommandHandler(IAdvertPackageRepository advertPackageRepository, IMapper mapper)
            {
                _advertPackageRepository = advertPackageRepository;
                _mapper = mapper;
            }
            public async Task<IResult> Handle(DeleteAdvertPackageCommand request, CancellationToken cancellationToken)
            {
                bool funcs = CheckEntities<IAdvertPackageRepository, AdvertPackage>.Exist(_advertPackageRepository, request.Id);
                if (!IfEngine.Engine(funcs))
                {
                    return new Result(ResultStatus.Warning, Messages.NotFound);
                }

                var result = await _advertPackageRepository.GetAsync(x => x.Id == request.Id);

                _advertPackageRepository.Delete(result);
                await _advertPackageRepository.SaveChangesAsync();

                return new Result(ResultStatus.Success, Messages.Deleted);
            }
        }
    }
}
