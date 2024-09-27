using AutoMapper;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Service.Common.Mappings;
using SWD.NextIntern.Service.Services.CampaignService.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignService
{
    public class CampaignDto : IMapFrom<Campaign>
    {
        public Guid CampaignId { get; set; }

        public int Id { get; set; }

        public string CampaignName { get; set; } = null!;

        public Guid? UniversityId { get; set; }

        public string? UniversityName { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string? CampaignState {  get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Campaign, CampaignDto>()
                .ForMember(dest => dest.UniversityName, opt => opt.MapFrom(src => src.University.UniversityName))
                .ForMember(dest => dest.CampaignState, opt => opt.MapFrom(src => src.CampaignState == 2 ? "Closed" : src.CampaignState == 1 ? "Opening" : "Not yet"));

        }
    }
}
