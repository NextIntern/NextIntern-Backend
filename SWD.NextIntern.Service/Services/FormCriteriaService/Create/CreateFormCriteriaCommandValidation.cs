using FluentValidation;
using SWD.NextIntern.Service.Services.FormCriteriaService.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.FormCriteriaService.Create
{
    public class CreateFormCriteriaCommandValidation : AbstractValidator<CreateFormCriteriaCommand>
    {
        public CreateFormCriteriaCommandValidation()
        {
            RuleFor(command => command.FormCriteriaName)
                 .MaximumLength(100).WithMessage("EvaluationFormId must not exceed 100 characters.");

            RuleFor(command => command.Guide)
                 .MaximumLength(1000).WithMessage("EvaluationFormId must not exceed 1000 characters.");

            RuleFor(command => command.MinScore)
                 .LessThanOrEqualTo(command => command.MaxScore).WithMessage("MinScore must be less than or equal to MaxScore")
                 .GreaterThanOrEqualTo(0).WithMessage("MinScore must be greater than or equal to 0");

            RuleFor(command => command.MaxScore)
                 .GreaterThanOrEqualTo(command => command.MinScore).WithMessage("MaxScore must be greater than or equal to MinScore")
                 .LessThanOrEqualTo(10).WithMessage("MaxScore must be less than or equal to 10");
        }

        private bool BeAValidGuid(string id)
        {
            return Guid.TryParse(id, out _);
        }

        private bool BeAValidGuidOrEmpty(string? id)
        {
            return string.IsNullOrEmpty(id) || Guid.TryParse(id, out _);
        }
    }
}
