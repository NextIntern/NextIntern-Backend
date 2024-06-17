
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
