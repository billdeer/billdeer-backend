using Billdeer.Business.Handlers.EntityExamples.Commands;
using Billdeer.Business.Handlers.EntityExamples.Queries;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.Entities.Concrete;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetEntityExampleQuery() { EntityExampleId = id };
            var result = await Mediator.Send(query);
            return SwitchMethod<EntityExampleDto, IDataResult<EntityExampleDto>>(result, "EntityExamples", "Get");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetEntityExamplesQuery();
            var result = await Mediator.Send(query);
            return SwitchMethod<IEnumerable<EntityExample>, IDataResult<IEnumerable<EntityExample>>>(result, "EntityExamples", "GetAll");
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateEntityExampleCommand request)
        {
            var result = await Mediator.Send(request);
            return SwitchMethod<EntityExampleDto, IDataResult<EntityExampleDto>>(result, "EntityExamples", "Add");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateEntityExampleCommand request)
        {
            var result = await Mediator.Send(request);
            return SwitchMethod<EntityExampleDto, IDataResult<EntityExampleDto>>(result, "EntityExamples", "Update");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteEntityExampleCommand request)
        {
            var result = await Mediator.Send(request);
            return SwitchMethod(result, "EntityExamples", "Delete");
        }
    }
}
