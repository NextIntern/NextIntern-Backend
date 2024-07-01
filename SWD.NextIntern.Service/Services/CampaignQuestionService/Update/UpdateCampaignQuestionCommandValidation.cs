using FluentValidation;
using SWD.NextIntern.Service.Services.InternEvaluationCriteriaService.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignQuestionService.Update
{
    public class UpdateCampaignQuestionCommandValidation : AbstractValidator<UpdateCampaignQuestionCommand>
    {
        public UpdateCampaignQuestionCommandValidation()
        {
            RuleFor(command => command.CampaignQuestionId)
                 .NotEmpty().WithMessage("Id is required.")
                 .Must(BeAValidGuid).WithMessage("CampaignQuestionId must be a valid GUID.");

            RuleFor(command => command.CampaignId)
                .NotEmpty().WithMessage("Id is required.")
                .Must(BeAValidGuid).WithMessage("CampaignId must be a valid GUID.");

            RuleFor(command => command.CampaignQuestion)
                .MaximumLength(100).WithMessage("CampaignQuestion must not exceed 100 characters.");

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
