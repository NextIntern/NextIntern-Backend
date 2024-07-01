using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignQuestionService.GetId
{
    public class GetCampaignQuestionByIdQuery : IRequest<ResponseObject<CampaignQuestionDto>>, IQuery
    {
        public string Id { get; set; }

        public GetCampaignQuestionByIdQuery(string id)
        {
            Id = id;
        }
    }
}
