using Billdeer.Business.Handlers.EntityExamples.Commands;
using Billdeer.Business.Handlers.EntityExamples.Queries;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.Entities.DTOs.EntityExampleDtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Billdeer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityExamplesController : BaseApiController
    {
        private readonly IMediator _mediator;

        public EntityExamplesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateEntityExampleCommand request)
        {
            //var result = await _mediator.Send(request);
            //SwitchMethod<EntityExampleDto, IDataResult<EntityExampleDto>>(result, "EntityExamples", "Add");
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetEntityExampleQuery() { EntityExampleId = id };
            return Ok(await _mediator.Send(query));
        }
    }
}
