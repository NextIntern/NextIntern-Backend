using FluentValidation;

namespace SWD.NextIntern.Service.Services.CampaignService.Update
{
    public class UpdateCampaignCommandValidation : AbstractValidator<UpdateCampaignCommand>
    {
        public UpdateCampaignCommandValidation()
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Must(BeAValidGuid).WithMessage("Id must be a valid GUID.");

            RuleFor(command => command.UniversityId)
                .Must(BeAValidGuid).WithMessage("UniversityId must be a valid GUID.");

            RuleFor(command => command.CampaignName)
                .NotEmpty().WithMessage("CampaignName is required.")
                .MaximumLength(100).WithMessage("CampaignName must not exceed 100 characters.");

            RuleFor(command => command.StartDate)
                .LessThan(x => x.EndDate).WithMessage("StartDate must be less than EndDate.")
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue);

            RuleFor(command => command.EndDate)
                .GreaterThan(x => x.StartDate).WithMessage("EndDate must be greater than StartDate.")
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue);
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
