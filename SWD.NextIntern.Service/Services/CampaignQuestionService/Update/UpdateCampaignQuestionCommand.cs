using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignQuestionService.Update
{
    public class UpdateCampaignQuestionCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public string CampaignQuestionId { get; set; }

        public string CampaignId { get; set; }

        public string CampaignQuestion { get; set; }

        [JsonIgnore]
        public DateTime? CreateDate { get; set; }

        [JsonIgnore]
        public DateTime? ModifyDate { get; set; }

        [JsonIgnore]
        public DateTime? DeletedDate { get; set; }
    }
}

