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
                .NotEmpty().WithMessage("Campaign name is required.")
                .Length(1, 100).WithMessage("Campaign name can't be longer than 100 characters.");

            RuleFor(x => x.StartDate)
                .NotNull().WithMessage("Start date is required.");

            RuleFor(x => x.EndDate)
                .NotNull().WithMessage("End date is required.");

            RuleFor(x => x)
                .Must(x => x.EndDate >= x.StartDate)
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
                .WithMessage("End date must be greater than or equal to start date.");
        }
    }
}
