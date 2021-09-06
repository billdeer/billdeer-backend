using Billdeer.Business.Handlers.OperationClaims.Commands;
using Billdeer.Core.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.OperationClaims.ValidationRules
{
    public class CreateOperationClaimValidator : AbstractValidator<CreateOperationClaimCommand>
    {
        public CreateOperationClaimValidator()
        {
            RuleFor(x => x.Name).MaximumLength(50);
            RuleFor(x => x.Alias).MaximumLength(50);
            RuleFor(x => x.Description).MaximumLength(100);
        }
    }
}
