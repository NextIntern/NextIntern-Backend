using FluentValidation;
using SWD.NextIntern.Service.Services.EvaluationFormService.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.EvaluationFormService.Create
{
    public class CreateEvaluationFormCommandValidation : AbstractValidator<CreateEvaluationFormCommand>
    {
        public CreateEvaluationFormCommandValidation()
        {
            RuleFor(command => command.IsActive)
                .Must(BeAValidBoolean).WithMessage("IsActive must be either true or false.");
        }

        private bool BeAValidGuid(string id)
        {
            return Guid.TryParse(id, out _);
        }

        private bool BeAValidGuidOrEmpty(string? id)
        {
            return string.IsNullOrEmpty(id) || Guid.TryParse(id, out _);
        }

        private bool BeAValidBoolean(bool isActive)
        {
            return isActive == true || isActive == false;
        }
    }
}
