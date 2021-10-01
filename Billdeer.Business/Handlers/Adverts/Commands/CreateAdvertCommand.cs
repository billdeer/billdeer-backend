using AutoMapper;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.Adverts.Commands
{
    public class CreateAdvertCommand : IRequest<IDataResult<Advert>>
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public class CreateAdvertCommandHandler : IRequestHandler<CreateAdvertCommand, IDataResult<Advert>>
        {
            private readonly IAdvertRepository _advertRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public CreateAdvertCommandHandler(IAdvertRepository advertRepository, IUserRepository userRepository, IMapper mapper)
            {
                _advertRepository = advertRepository;
                _userRepository = userRepository;
                _mapper = mapper;
            }

            [RemoveCacheAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<Advert>> Handle(CreateAdvertCommand request, CancellationToken cancellationToken)
            {
                if (!IfEngine.Engine(CheckEntities<IUserRepository, User>.Exist(_userRepository, request.UserId)))
                {
                    return new DataResult<Advert>(ResultStatus.Warning, Messages.UserNotFound);
                }

                var addedAdvert = _mapper.Map<Advert>(request);

                _advertRepository.Add(addedAdvert);
                await _advertRepository.SaveChangesAsync();

                return new DataResult<Advert>(addedAdvert, ResultStatus.Success, Messages.Added);
            }
        }
    }
}
