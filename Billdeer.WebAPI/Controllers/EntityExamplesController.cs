using Billdeer.Business.Handlers.EntityExamples.Commands;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.Core.Utilities.Results.Concrete;
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
    public class EntityExamplesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public async Task<IActionResult> AddAsync(CreateEntityExampleCommand request)
        {
            var result = await _mediator.Send(request);

            switch (result.ResultStatus)
            {
                case ResultStatus.Warning:
                    return Ok(new ApiDataResult<EntityExampleDto>
                    {
                        HttpStatusCode = HttpStatusCode.OK,
                        URI = Url.Link("", new { Controller = "EntityExamples", Action = "Add" }),
                        Message = result.Message,
                        InternalMessage = null,
                        Errors = null,
                        Data = result.Data
                    });
                default:
                    return Ok(new ApiDataResult<EntityExampleDto>
                    {
                        HttpStatusCode = HttpStatusCode.OK,
                        URI = Url.Link("", new { Controller = "EntityExamples", Action = "Add" }),
                        Message = result.Message,
                        InternalMessage = null,
                        Errors = null,
                        Data = result.Data
                    });
            }
        }
    }
}
