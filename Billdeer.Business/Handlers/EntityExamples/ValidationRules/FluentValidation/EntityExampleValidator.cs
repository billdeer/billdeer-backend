using Billdeer.Business.Handlers.EntityExamples.Commands;
using Billdeer.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.EntityExamples.ValidationRules.FluentValidation
{
    public class CreateEntityExampleValidator : AbstractValidator<CreateEntityExampleCommand>
    {
        public CreateEntityExampleValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Name).Must(EndWithBD);
        }

        public static bool EndWithBD(string arg)
        {
            return arg.EndsWith("BD");
        }
    }

    public class UpdateEntityExampleValidator : AbstractValidator<UpdateEntityExampleCommand>
    {
        public UpdateEntityExampleValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Name).Must(CreateEntityExampleValidator.EndWithBD);
        }
    }
}
