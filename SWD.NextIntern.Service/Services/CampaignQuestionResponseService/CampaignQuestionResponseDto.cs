using AutoMapper;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Service.Common.Mappings;
using System.Text.Json.Serialization;

namespace SWD.NextIntern.Service.Services.CampaignQuestionResponseService
{
    public class CampaignQuestionResponseDto : IMapFrom<CampaignQuestionResponse>
    {
        public Guid CampaignQuestionResponseId { get; set; }

        public Guid CampaignQuestionId { get; set; }

        public Guid InternId { get; set; }

        public string Response { get; set; }

        public int Rating { get; set; }

        [JsonIgnore]
        public DateTime? DeletedDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CampaignQuestionResponse, CampaignQuestionResponseDto>();
        }
    }
}
