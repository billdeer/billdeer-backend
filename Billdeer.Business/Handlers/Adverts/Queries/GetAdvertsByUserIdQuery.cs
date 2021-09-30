using Billdeer.Business.Constants;
using Billdeer.Core.Entities.Concrete;
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

namespace Billdeer.Business.Handlers.Adverts.Queries
{
    public class GetAdvertsByUserIdQuery : IRequest<IDataResult<IEnumerable<Advert>>>
    {
        public long UserId { get; set; }

        public class GetAdvertsByUserIdQueryHandler : IRequestHandler<GetAdvertsByUserIdQuery, IDataResult<IEnumerable<Advert>>>
        {
            private readonly IAdvertRepository _advertRepository;
            private readonly IUserRepository _userRepository;

            public GetAdvertsByUserIdQueryHandler(IAdvertRepository advertRepository, IUserRepository userRepository)
            {
                this._advertRepository = advertRepository;
                _userRepository = userRepository;
            }

            public async Task<IDataResult<IEnumerable<Advert>>> Handle(GetAdvertsByUserIdQuery request, CancellationToken cancellationToken)
            {
                if (!IfEngine.Engine(CheckEntities<IUserRepository, User>.Exist(_userRepository, request.UserId)))
                {
                    return new DataResult<IEnumerable<Advert>>(ResultStatus.Warning, Messages.UserNotFound);
                }

                var advert = await _advertRepository.GetListAsync(x => x.UserId == request.UserId && x.IsActive == true && x.IsDeleted == false);

                if (advert is null)
                {
                    return new DataResult<IEnumerable<Advert>>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<IEnumerable<Advert>>(advert, ResultStatus.Success, Messages.Success);
            }
        }
    }
}
