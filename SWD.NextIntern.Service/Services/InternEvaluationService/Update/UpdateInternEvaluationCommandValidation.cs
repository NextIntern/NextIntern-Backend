using FluentValidation;

namespace SWD.NextIntern.Service.Services.InternEvaluationService.Update
{
    public class UpdateInternEvaluationCommandValidation : AbstractValidator<UpdateInternEvaluationCommand>
    {
        public UpdateInternEvaluationCommandValidation()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(BeAValidGuid).WithMessage("Id must be a valid GUID.");

            RuleFor(x => x.InternId)
                .NotEmpty().WithMessage("InternId is required.")
                .Must(BeAValidGuid).WithMessage("InternId must be a valid GUID.");

            RuleFor(x => x.CampaignEvaluationId)
                .NotEmpty().WithMessage("CampaignEvaluationId is required.")
                .Must(BeAValidGuid).WithMessage("CampaignEvaluationId must be a valid GUID.");

            RuleFor(x => x.Feedback)
                .MaximumLength(1000).WithMessage("Feedback cannot exceed 1000 characters.");
        }

        private bool BeAValidGuid(string? guid)
        {
            return Guid.TryParse(guid, out _);
        }
    }
}
