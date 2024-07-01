using FluentValidation;
using SWD.NextIntern.Service.Services.CampaignQuestionService.Delete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignQuestionResponseService.Delete
{
    public class DeleteCampaignQuestionResponseCommandValidation : AbstractValidator<DeleteCampaignQuestionResponseCommand>
    {
        public DeleteCampaignQuestionResponseCommandValidation()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Id can not be null");
        }
    }
}
