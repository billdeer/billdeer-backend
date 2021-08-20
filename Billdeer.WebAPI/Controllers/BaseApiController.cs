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
        /// <summary>
        /// Which will be used when data is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="result"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns>
        /// ...
        /// </returns>
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


        /// <summary>
        /// Which will be used when no data is returned.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="result"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns>
        /// ...
        /// </returns>
        [NonAction]
        public IActionResult SwitchMethod<TResult>(TResult result, string controllerName, string actionName) where TResult : IResult
        {
            switch (result.ResultStatus)
            {
                case ResultStatus.Warning:
                    return Warning<TResult>(result, controllerName, actionName);
                default:
                    return Success<TResult>(result, controllerName, actionName);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">
        /// T is Data Type
        /// </typeparam>
        /// <typeparam name="TResult">
        /// TResult is Result Type
        /// </typeparam>
        /// <param name="result"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="result"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult Success<TResult>(TResult result, string controllerName, string actionName) where TResult : IResult
        {
            return Ok(new ApiResult
            {
                HttpStatusCode = HttpStatusCode.OK,
                URL = Url.Link("", new { Controller = controllerName, Action = actionName }),
                Message = result.Message,
                InternalMessage = null,
                Errors = null
            });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="result"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="result"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult Warning<TResult>(TResult result, string controllerName, string actionName) where TResult : IResult
        {
            return BadRequest(new ApiResult
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                URL = Url.Link("", new { Controller = controllerName, Action = actionName }),
                Message = result.Message,
                InternalMessage = null,
                Errors = null
            });
        }
    }
}

