﻿using AutoMapper;
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
    public class DeleteAdvertCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class DeleteAdvertCommandHandler : IRequestHandler<DeleteAdvertCommand, IResult>
        {
            private readonly IAdvertRepository _advertRepository;

            public DeleteAdvertCommandHandler(IAdvertRepository advertRepository)
            {
                _advertRepository = advertRepository;
            }

            [RemoveCacheAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteAdvertCommand request, CancellationToken cancellationToken)
            {
                bool funcs = CheckEntities<IAdvertRepository, Advert>.Exist(_advertRepository, request.Id);
                if (!IfEngine.Engine(funcs))
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
