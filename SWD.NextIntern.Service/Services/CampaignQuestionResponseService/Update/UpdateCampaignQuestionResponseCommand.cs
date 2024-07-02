using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignQuestionResponseService.Update
{
    public class UpdateCampaignQuestionResponseCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public string CampaignQuestionResponseId { get; set; }

        public string CampaignQuestionId { get; set; }

        public string InternId { get; set; }

        public string Response { get; set; }

        public int Rating { get; set; }

        [JsonIgnore]
        public DateTime? DeletedDate { get; set; }

    }
}
