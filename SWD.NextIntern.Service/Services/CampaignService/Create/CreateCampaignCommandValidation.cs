using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignService.Create
{
    public class CreateCampaignCommandValidation : AbstractValidator<CreateCampaignCommand>
    {
        public CreateCampaignCommandValidation()
        {
            RuleFor(x => x.CampaignName)
                .NotEmpty().WithMessage("Campaign name is required.");

            RuleFor(x => x.UniversityId)
                .Must(BeAValidGuid).When(x => !string.IsNullOrEmpty(x.UniversityId)).WithMessage("University ID must be a valid GUID.");

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
