using AutoMapper;
using Billdeer.Business.Constants;
using Billdeer.Core.Aspects.Autofac.Caching;
using Billdeer.Core.Aspects.Autofac.Logging;
using Billdeer.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
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
    public class UpdateAdvertCommand : IRequest<IDataResult<Advert>>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public class UpdateAdvertCommandHandler : IRequestHandler<UpdateAdvertCommand, IDataResult<Advert>>
        {
            private readonly IAdvertRepository _advertRepository;
            private readonly IMapper _mapper;

            public UpdateAdvertCommandHandler(IAdvertRepository advertRepository, IMapper mapper)
            {
                _advertRepository = advertRepository;
                _mapper = mapper;
            }

            [RemoveCacheAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<Advert>> Handle(UpdateAdvertCommand request, CancellationToken cancellationToken)
            {
                if (!IfEngine.Engine(CheckEntities<IAdvertRepository, Advert>.Exist(_advertRepository, request.Id)))
                {
                    return new DataResult<Advert>(ResultStatus.Warning, Messages.NotFound);
                }

                var updatedAdvert = await _advertRepository.GetAsync(x => x.Id == request.Id);
                updatedAdvert.ModifiedDate = DateTime.Now;
                updatedAdvert.Name = request.Name;
                updatedAdvert.Description = request.Description;

                _advertRepository.Update(updatedAdvert);
                await _advertRepository.SaveChangesAsync();

                return new DataResult<Advert>(updatedAdvert, ResultStatus.Success, Messages.Updated);

            }
        }
    }
}
