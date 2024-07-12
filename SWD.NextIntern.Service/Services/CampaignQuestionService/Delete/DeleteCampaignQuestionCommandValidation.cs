using FluentValidation;
using SWD.NextIntern.Service.Services.CampaignService.Delete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignQuestionService.Delete
{
    public class DeleteCampaignQuestionCommandValidation : AbstractValidator<DeleteCampaignQuestionCommand>
    {
        public DeleteCampaignQuestionCommandValidation()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Id can not be null");
        }
    }
}
