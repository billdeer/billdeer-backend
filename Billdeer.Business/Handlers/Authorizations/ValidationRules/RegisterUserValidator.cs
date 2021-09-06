using Billdeer.Business.Handlers.Authorizations.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.Authorizations.ValidationRules
{

    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(p => p.Password).Password();
        }
    }
}
