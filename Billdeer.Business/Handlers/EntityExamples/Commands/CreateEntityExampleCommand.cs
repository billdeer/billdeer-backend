﻿using AutoMapper;
using Billdeer.Business.AutoMapper.Profiles;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.DataAccess.Abstract;
using Billdeer.Entities.Concrete;
using Billdeer.Entities.DTOs.EntityExampleDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.EntityExamples.Commands
{
    public class CreateEntityExampleCommand : IRequest<IDataResult<EntityExampleDto>>
    {
        public string Name { get; set; }
            
        public class CreateEntityExampleCommandHandler : IRequestHandler<CreateEntityExampleCommand, IDataResult<EntityExampleDto>>
        {
            IEntityExampleRepository _entityExampleRepository;
            private readonly IMapper _mapper;

            public CreateEntityExampleCommandHandler(IEntityExampleRepository entityExampleRepository, IMapper mapper)
            {
                _entityExampleRepository = entityExampleRepository;
                this._mapper = mapper;
            }

            public async Task<IDataResult<EntityExampleDto>> Handle(CreateEntityExampleCommand request, CancellationToken cancellationToken)
            {
                var entity = await _entityExampleRepository.GetAsync(x => x.Name == request.Name);

                if (entity is not null)
                    return new DataResult<EntityExampleDto>(ResultStatus.Warning);

                var entityForDb = _mapper.Map<EntityExample>(request);

                //var entityForDb = new EntityExample
                //{
                //    Name = request.Name,
                //    CreatedDate = DateTime.Now,
                //    IsDeleted = false,
                //    IsActive = true
                //};

                var entityAdded = _entityExampleRepository.Add(entityForDb);
                await _entityExampleRepository.SaveChangesAsync();

                var entityResponseDto = _mapper.Map<EntityExampleDto>(entityAdded);

                return new DataResult<EntityExampleDto>(entityResponseDto, ResultStatus.Success);
            }
        }
        
    }
}
