using Billdeer.Business.BusinessAspects.Autofac;
using Billdeer.Business.Constants;
using Billdeer.Business.Handlers.Authorizations.ValidationRules;
using Billdeer.Core.Aspects.Autofac.Logging;
using Billdeer.Core.Aspects.Autofac.Validation;
using Billdeer.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Billdeer.Core.Entities.Concrete;
using Billdeer.Core.Utilities.Mail;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.Core.Utilities.Security.Hashing;
using Billdeer.DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.Authorizations.Commands
{
    public class RegisterUserCommand : IRequest<IResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IResult>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMailService _mailService;

            public RegisterUserCommandHandler(IUserRepository userRepository, IMailService mailService)
            {
                _userRepository = userRepository;
                _mailService = mailService;
            }

            [ValidationAspect(typeof(RegisterUserValidator), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                var userExist = await _userRepository.GetAsync(u => u.Email == request.Email);

                if (userExist is not null)
                    return new Result(ResultStatus.Warning , Messages.UserAlreadyExists);


                HashingHelper.CreatePasswordHash(request.Password, out var passwordSalt, out var passwordHash);
                var user = new User
                {
                    Email = request.Email,

                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Username = request.Username,
                    Status = true
                };
                ////.
                //EmailAddress emailAddressTo = new EmailAddress();
                //emailAddressTo.Name = "eldem";
                //emailAddressTo.Address = "eldemog.me@gmail.com";

                //var toList = new List<EmailAddress>();
                //toList.Add(emailAddressTo);
                ////..
                //EmailAddress emailAddressFrom = new EmailAddress();
                //emailAddressFrom.Name = "eldem";
                //emailAddressFrom.Address = "billdeer@gmail.com";

                //var fromList = new List<EmailAddress>();
                //fromList.Add(emailAddressFrom);
                ////...
                //EmailMessage emailMessage = new EmailMessage();
                //emailMessage.ToAddresses = toList;
                //emailMessage.FromAddresses = fromList;
                //emailMessage.Subject = "Deneme";
                //emailMessage.Content = "Deneme 1";

                //_mailService.Send(emailMessage);

                _userRepository.Add(user);
                await _userRepository.SaveChangesAsync();

                return new Result(ResultStatus.Success, Messages.Added);
            }
        }
    }
}
