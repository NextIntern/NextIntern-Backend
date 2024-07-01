using FluentValidation;
using SWD.NextIntern.Service.Services.CampaignQuestionService.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignQuestionResponseService.Create
{
    public class CreateCampaignQuestionResponseCommandValidation : AbstractValidator<CreateCampaignQuestionResponseCommand>
    {
        public CreateCampaignQuestionResponseCommandValidation()
        {
            RuleFor(x => x.CampaignQuestionId)
                .Must(BeAValidGuid).When(x => !string.IsNullOrEmpty(x.CampaignQuestionId)).WithMessage("CampaignQuestion ID must be a valid GUID.");

            RuleFor(x => x.InternId)
               .Must(BeAValidGuid).When(x => !string.IsNullOrEmpty(x.InternId)).WithMessage("Intern ID must be a valid GUID.");

            RuleFor(command => command.Response)
                .MaximumLength(1000).WithMessage("Response must not exceed 1000 characters.");
        }

        private bool BeAValidGuid(string? id)
        {
            return Guid.TryParse(id, out _);
        }
    }
}
