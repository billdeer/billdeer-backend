using Billdeer.Business.Constants;
using Billdeer.Core.Entities.Concrete;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.Core.Utilities.ToolKit;
using Billdeer.DataAccess.Abstract;
using Billdeer.Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.Adverts.Queries
{
    public class GetDeactivatedAdvertsByUserIdQuery : IRequest<IDataResult<IEnumerable<Advert>>>
    {
        public long UserId { get; set; }

        public class GetDeactivatedAdvertsByUserIdQueryHandler : IRequestHandler<GetDeactivatedAdvertsByUserIdQuery, IDataResult<IEnumerable<Advert>>>
        {
            private readonly IAdvertRepository _advertRepository;
            private readonly IUserRepository _userRepository;

            public GetDeactivatedAdvertsByUserIdQueryHandler(IAdvertRepository advertRepository, IUserRepository userRepository)
            {
                _advertRepository = advertRepository;
                _userRepository = userRepository;
            }

            public async Task<IDataResult<IEnumerable<Advert>>> Handle(GetDeactivatedAdvertsByUserIdQuery request, CancellationToken cancellationToken)
            {
                if (IfEngine.Engine(CheckEntities<IUserRepository, User>.Exist(_userRepository, request.UserId)))
                {
                    return new DataResult<IEnumerable<Advert>>(ResultStatus.Warning, Messages.NotFound);
                }

                var deactivatedAdverts = await _advertRepository.GetListAsync(x => x.UserId == request.UserId && x.IsActive == false && x.IsDeleted == false);

                if (deactivatedAdverts is null)
                {
                    return new DataResult<IEnumerable<Advert>>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<IEnumerable<Advert>>(deactivatedAdverts, ResultStatus.Success, Messages.Success);
            }
        }
    }
}
