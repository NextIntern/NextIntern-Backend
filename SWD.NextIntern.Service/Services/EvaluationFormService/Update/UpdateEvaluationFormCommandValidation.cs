
using FluentValidation;

namespace SWD.NextIntern.Service.Services.EvaluationFormService.Update
{
    public class UpdateEvaluationFormCommandValidation : AbstractValidator<UpdateEvaluationFormCommand>
    {
        public UpdateEvaluationFormCommandValidation()
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Must(BeAValidGuid).WithMessage("Id must be a valid GUID.");

            RuleFor(command => command.UniversityId)
                .Must(BeAValidGuidOrEmpty).WithMessage("UniversityId must be a valid GUID or empty.");
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
