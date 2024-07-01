using AutoMapper;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Service.Common.Mappings;
using System.Text.Json.Serialization;

namespace SWD.NextIntern.Service.Services.CampaignQuestionService
{
    public class CampaignQuestionDto : IMapFrom<CampaignQuestion>
    {
        public Guid CampaignQuestionId { get; set; }

        public Guid CampaignId { get; set; }

        public string Question { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        [JsonIgnore]
        public DateTime? DeletedDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CampaignQuestion, CampaignQuestionDto>();
        }
    }
}
