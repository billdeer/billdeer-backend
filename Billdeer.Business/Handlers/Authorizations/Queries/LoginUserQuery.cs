using Billdeer.Business.Constants;
using Billdeer.Core.Aspects.Autofac.Logging;
using Billdeer.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Billdeer.Core.Entities.Dtos.UserDtos;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.Core.Utilities.Security.Hashing;
using Billdeer.Core.Utilities.Security.JWT;
using Billdeer.DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.Authorizations.Queries
{
    public class LoginUserQuery : IRequest<IDataResult<UserLoginResDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, IDataResult<UserLoginResDto>>
        {
            private readonly IUserRepository _userRepository;
            private readonly ITokenHelper _tokenHelper;
            //private readonly ICacheManager _cacheManager;

            public LoginUserQueryHandler(IUserRepository userRepository, ITokenHelper tokenHelper)
            {
                _userRepository = userRepository;
                _tokenHelper = tokenHelper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<UserLoginResDto>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(u => u.Email == request.Email && u.Status);

                if (user is null)
                    return new DataResult<UserLoginResDto>(ResultStatus.Warning, Messages.UserNotFound);

                if (!HashingHelper.VerifyPasswordHash(request.Password, user.PasswordSalt, user.PasswordHash))
                    return new DataResult<UserLoginResDto>(ResultStatus.Authentication, Messages.PasswordError);

                var claims = _userRepository.GetClaims(user);

                var accessToken = _tokenHelper.CreateToken(user, claims);

                var userLoginResDto = new UserLoginResDto
                {
                    accessToken = accessToken,
                    Email = user.Email,
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };

                return new DataResult<UserLoginResDto>(userLoginResDto, ResultStatus.Success, Messages.SuccessfulLogin);

            }
        }
    }
}
