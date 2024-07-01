using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignQuestionResponseService.Update
{
    public class UpdateCampaignQuestionResponseCommandValidation : AbstractValidator<UpdateCampaignQuestionResponseCommand>
    {
        public UpdateCampaignQuestionResponseCommandValidation()
        {
            RuleFor(command => command.CampaignQuestionId)
                 .NotEmpty().WithMessage("Id is required.")
                 .Must(BeAValidGuid).WithMessage("CampaignQuestionId must be a valid GUID.");

            RuleFor(command => command.InternId)
                .NotEmpty().WithMessage("Id is required.")
                .Must(BeAValidGuid).WithMessage("InternId must be a valid GUID.");

            RuleFor(command => command.Response)
                .MaximumLength(1000).WithMessage("CampaignQuestion must not exceed 1000 characters.");

            RuleFor(command => command.Rating)
               .GreaterThanOrEqualTo(0).WithMessage("Rating must be greater than or equal to 0")
                .LessThanOrEqualTo(10).WithMessage("Rating must be less than or equal to 10");

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
