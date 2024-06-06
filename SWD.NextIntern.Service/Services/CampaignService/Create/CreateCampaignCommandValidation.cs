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
        }
    }
}
