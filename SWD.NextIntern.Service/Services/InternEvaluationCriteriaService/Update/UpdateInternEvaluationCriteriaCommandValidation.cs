
using FluentValidation;

namespace SWD.NextIntern.Service.Services.InternEvaluationCriteriaService.Update
{
    public class UpdateInternEvaluationCriteriaCommandValidation : AbstractValidator<UpdateInternEvaluationCriteriaCommand>
    {
        public UpdateInternEvaluationCriteriaCommandValidation()
        {
            RuleFor(command => command.InternEvaluationCriteriaId)
                 .NotEmpty().WithMessage("Id is required.")
                 .Must(BeAValidGuid).WithMessage("InternEvaluationCriteriaId must be a valid GUID.");

            RuleFor(command => command.InternEvaluationId)
                .NotEmpty().WithMessage("Id is required.")
                .Must(BeAValidGuid).WithMessage("InternEvaluationId must be a valid GUID.");

            RuleFor(command => command.FromCriteriaId)
                .NotEmpty().WithMessage("Id is required.")
                .Must(BeAValidGuid).WithMessage("FromCriteriaId must be a valid GUID.");

            RuleFor(command => command.Score)
               .GreaterThanOrEqualTo(0).WithMessage("Score must be greater than or equal to 0")
                .LessThanOrEqualTo(10).WithMessage("Score must be less than or equal to 10");

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
