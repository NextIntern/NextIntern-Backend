
using FluentValidation;

namespace SWD.NextIntern.Service.Services.InternService.Update
{
    public class UpdateInternCommandValidation : AbstractValidator<UpdateInternCommand>
    {
        public UpdateInternCommandValidation()
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Must(BeAValidGuid).WithMessage("Id must be a valid GUID.");

            RuleFor(command => command.CampaignId)
                .Must(BeAValidGuidOrEmpty).WithMessage("CampaignId must be a valid GUID.");           
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
