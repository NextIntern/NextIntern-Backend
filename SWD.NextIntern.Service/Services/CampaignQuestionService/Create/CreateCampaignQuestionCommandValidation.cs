using FluentValidation;
using SWD.NextIntern.Service.Services.CampaignService.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignQuestionService.Create
{
    public class CreateCampaignQuestionCommandValidation : AbstractValidator<CreateCampaignQuestionCommand>
    {
        public CreateCampaignQuestionCommandValidation()
        {
            RuleFor(x => x.CampaignId)
                .Must(BeAValidGuid).When(x => !string.IsNullOrEmpty(x.CampaignId)).WithMessage("University ID must be a valid GUID.");

            RuleFor(command => command.CampaignQuestion)
                .MaximumLength(100).WithMessage("CampaignQuestion must not exceed 100 characters.");
        }

        private bool BeAValidGuid(string? id)
        {
            return Guid.TryParse(id, out _);
        }
    }
}
