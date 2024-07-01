using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignQuestionService.GetId
{
    public class GetCampaignQuestionByIdQueryValidation : AbstractValidator<GetCampaignQuestionByIdQuery>
    {
        public GetCampaignQuestionByIdQueryValidation()
        {
            RuleFor(query => query.Id).NotEmpty().WithMessage("Id is not null");
        }
    }
}
