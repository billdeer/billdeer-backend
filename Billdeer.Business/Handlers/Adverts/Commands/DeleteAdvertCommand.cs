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
    public class DeleteAdvertCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class DeleteAdvertCommandHandler : IRequestHandler<DeleteAdvertCommand, IResult>
        {
            private readonly IAdvertRepository _advertRepository;
            private readonly IMapper _mapper;

            public DeleteAdvertCommandHandler(IAdvertRepository advertRepository, IMapper mapper)
            {
                _advertRepository = advertRepository;
                _mapper = mapper;
            }

            public async Task<IResult> Handle(DeleteAdvertCommand request, CancellationToken cancellationToken)
            {
                if (!IfEngine.Engine(CheckEntities<IAdvertRepository, Advert>.Exist(_advertRepository, request.Id)))
                {
                    return new Result(ResultStatus.Warning, Messages.NotFound);
                }

                var result = await _advertRepository.GetAsync(x => x.Id == request.Id);

                _advertRepository.Delete(result);
                await _advertRepository.SaveChangesAsync();

                return new Result(ResultStatus.Success, Messages.Deleted);
            }

        }
    }
}
