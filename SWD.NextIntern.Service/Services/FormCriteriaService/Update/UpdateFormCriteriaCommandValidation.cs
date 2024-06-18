
using FluentValidation;

namespace SWD.NextIntern.Service.Services.FormCriteriaService.Update
{
    public class UpdateFormCriteriaCommandValidation : AbstractValidator<UpdateFormCriteriaCommand>
    {
        public UpdateFormCriteriaCommandValidation()
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Must(BeAValidGuid).WithMessage("Id must be a valid GUID.");

            RuleFor(command => command.EvaluationFormId)
                .Must(BeAValidGuidOrEmpty).WithMessage("EvaluationFormId must be a valid GUID or empty.");

            RuleFor(command => command.FormCriteriaName)
                    .MaximumLength(100).WithMessage("EvaluationFormId must not exceed 100 characters.");

            RuleFor(command => command.Guide)
                    .MaximumLength(1000).WithMessage("EvaluationFormId must not exceed 1000 characters.");

            RuleFor(command => command.MinScore)
                .GreaterThanOrEqualTo(0).WithMessage("MinScore must be greater than or equal to 0")
                .LessThanOrEqualTo(10).WithMessage("MaxScore must be less than or equal to 0");
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
