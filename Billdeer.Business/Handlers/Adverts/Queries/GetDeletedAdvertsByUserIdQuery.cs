using Billdeer.Business.Constants;
using Billdeer.Core.Aspects.Autofac.Caching;
using Billdeer.Core.Aspects.Autofac.Logging;
using Billdeer.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
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
    public class GetDeletedAdvertsByUserIdQuery : IRequest<IDataResult<IEnumerable<Advert>>>
    {
        public long UserId { get; set; }
        public class GetDeletedAdvertsByUserIdQueryHandler : IRequestHandler<GetDeletedAdvertsByUserIdQuery, IDataResult<IEnumerable<Advert>>>
        {
            private readonly IAdvertRepository _advertRepository;
            private readonly IUserRepository _userRepository;

            public GetDeletedAdvertsByUserIdQueryHandler(IAdvertRepository advertRepository, IUserRepository userRepository)
            {
                _advertRepository = advertRepository;
                _userRepository = userRepository;
            }

            [CacheAspect]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Advert>>> Handle(GetDeletedAdvertsByUserIdQuery request, CancellationToken cancellationToken)
            {
                if (!IfEngine.Engine(CheckEntities<IUserRepository, User>.Exist(_userRepository, request.UserId)))
                {
                    return new DataResult<IEnumerable<Advert>>(ResultStatus.Warning, Messages.UserNotFound);
                }

                var advert = await _advertRepository.GetListAsync(x => x.UserId == request.UserId && x.IsActive == false && x.IsDeleted == true);

                if (advert is null)
                {
                    return new DataResult<IEnumerable<Advert>>(ResultStatus.Warning, Messages.NotFound);
                }

                return new DataResult<IEnumerable<Advert>>(advert, ResultStatus.Success, Messages.Success);
            }

        }
    }
}
