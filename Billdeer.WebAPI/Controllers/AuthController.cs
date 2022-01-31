using Billdeer.Business.Handlers.Authorizations.Commands;
using Billdeer.Business.Handlers.Authorizations.Queries;
using Billdeer.Core.Entities.Dtos.UserDtos;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billdeer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand registerUser)
        {
            var result = await Mediator.Send(registerUser);

            return SwitchMethod(result, "Auth", "Register");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserQuery loginModel)
        {
            var result = await Mediator.Send(loginModel);

            return SwitchMethod<UserLoginResDto, IDataResult<UserLoginResDto>>(result, "Auth", "Login");
        }
    }
}
