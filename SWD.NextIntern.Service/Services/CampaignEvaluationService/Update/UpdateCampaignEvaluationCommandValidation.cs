using FluentValidation;

namespace SWD.NextIntern.Service.Services.CampaignEvaluationService.Update
{
    public class UpdateCampaignEvaluationCommandValidation : AbstractValidator<UpdateCampaignEvaluationCommand>
    {
        public UpdateCampaignEvaluationCommandValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Must(BeAValidGuid).WithMessage("Id must be a valid GUID.");

            RuleFor(x => x.CampaignId)
                .NotEmpty().WithMessage("Campaign ID is required.")
                .Must(BeAValidGuid).WithMessage("Campaign ID must be a valid GUID.");

            RuleFor(x => x.StartDate)
                .NotNull().WithMessage("Start date is required.");

            RuleFor(x => x.EndDate)
                .NotNull().WithMessage("End date is required.")
                .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("End date must be on or after the start date.");
        }

        private bool BeAValidGuid(string? id)
        {
            return Guid.TryParse(id, out _);
        }
    }
}
