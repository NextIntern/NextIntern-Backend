using FluentValidation;
using SWD.NextIntern.Service.Services.CampaignQuestionService.GetId;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignQuestionResponseService.GetId
{
    public class GetCampaignQuestionResponseByIdQueryValidation : AbstractValidator<GetCampaignQuestionResponseByIdQuery>
    {
        public GetCampaignQuestionResponseByIdQueryValidation()
        {
            RuleFor(query => query.Id).NotEmpty().WithMessage("Id is not null");
        }
    }
}
