﻿using AutoMapper;
using Billdeer.Business.AutoMapper.Profiles;
using Billdeer.Core.Utilities.Results;
using Billdeer.DataAccess.Abstract;
using Billdeer.Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.EntityExamples.Commands
{
    public class CreateEntityExampleCommand : IRequest<IDataResult<EntityExample>>
    {
        public string Name { get; set; } // Burada controllerdaki post metodundan gelicek olan propertyler olucak.
        // Aslında bazı dtolara gerek kalmaya bilir ama map işlemi lazım burada da, controllerdan alırken normal entity'e çevirip dbye yollamak gericek.

        public class CreateEntityExampleCommandHandler : IRequestHandler<CreateEntityExampleCommand, IDataResult<EntityExample>>
        {
            private readonly IEntityExampleRepository _entityExampleRepository;
            private readonly IMapper _mapper;

            public CreateEntityExampleCommandHandler(IEntityExampleRepository entityExampleRepository, IMapper mapper)
            {
                _entityExampleRepository = entityExampleRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<EntityExample>> Handle(CreateEntityExampleCommand request, CancellationToken cancellationToken)
            {
                var entity = await _entityExampleRepository.GetAsync(x => x.Name == request.Name);

                if (entity is not null)
                    new ErrorResult();

                var entityMap = _mapper.Map<CreateEntityExampleCommand, EntityExample>(request);

                var result = _entityExampleRepository.Add(entityMap);
                await _entityExampleRepository.SaveChangesAsync();

                return new SuccessDataResult<EntityExample>(result);
            }
        }
    }
}