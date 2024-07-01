using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignQuestionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignQuestionResponseService.GetId
{
    public class GetCampaignQuestionResponseByIdQuery : IRequest<ResponseObject<CampaignQuestionResponseDto>>, IQuery
    {
        public string Id { get; set; }

        public GetCampaignQuestionResponseByIdQuery(string id)
        {
            Id = id;
        }
    }
}
