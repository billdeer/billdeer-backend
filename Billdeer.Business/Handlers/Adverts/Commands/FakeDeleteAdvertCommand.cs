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

namespace Billdeer.Business.Handlers.Adverts.Commands
{
    public class FakeDeleteAdvertCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class FakeDeleteAdvertCommandHandler : IRequestHandler<FakeDeleteAdvertCommand, IResult>
        {
            private readonly IAdvertRepository _advertRepository;
            private readonly IMapper _mapper;

            public FakeDeleteAdvertCommandHandler(IAdvertRepository advertRepository, IMapper mapper)
            {
                _advertRepository = advertRepository;
                _mapper = mapper;
            }

            public async Task<IResult> Handle(FakeDeleteAdvertCommand request, CancellationToken cancellationToken)
            {

                if (!IfEngine.Engine(CheckEntities<IAdvertRepository, Advert>.Exist(_advertRepository, request.Id)))
                {
                    return new Result(ResultStatus.Warning, Messages.NotFound);
                }

                var advert = await _advertRepository.GetAsync(x => x.Id == request.Id);

                advert.IsDeleted = true;
                advert.DeletedDate = DateTime.Now;
                advert.IsActive = false;

                _advertRepository.Update(advert);
                await _advertRepository.SaveChangesAsync();

                return new Result(ResultStatus.Success, Messages.Deleted);
            }

        }
    }
}
