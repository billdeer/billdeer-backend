using Billdeer.Core.Entities.Abstract;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.Entities.DTOs.EntityExampleDtos;
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
    public class BaseApiController : ControllerBase
    {

        [NonAction]
        public IActionResult SwitchMethod<T, TResult>(TResult result, string controllerName, string actionName) where TResult : IDataResult<T>
        {
            switch (result.ResultStatus)
            {
                case ResultStatus.Warning:
                    return Warning<T, TResult>(result, controllerName, actionName);
                default:
                    return Success<T, TResult>(result, controllerName, actionName);
            }
        }

        [NonAction]
        public IActionResult Success<T, TResult>(TResult result, string controllerName, string actionName) where TResult : IDataResult<T>
        {
            return Ok(new ApiResult
            {
                HttpStatusCode = HttpStatusCode.OK,
                URL = Url.Link("", new { Controller = controllerName, Action = actionName }),
                Message = result.Message,
                InternalMessage = null,
                Errors = null,
                Data = result.Data
            });
        }    
        
        [NonAction]
        public IActionResult Warning<T, TResult>(TResult result, string controllerName, string actionName) where TResult : IDataResult<T>
        {
            return BadRequest(new ApiResult
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                URL = Url.Link("", new { Controller = controllerName, Action = actionName }),
                Message = result.Message,
                InternalMessage = null,
                Errors = null,
                Data = result.Data
            });
        }
    }
}

