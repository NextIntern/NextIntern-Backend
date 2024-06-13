using AutoMapper;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Service.Services.CampaignService;
using SWD.NextIntern.Service.Services.UniversityService;

namespace SWD.NextIntern.Service.Common.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Campaign
            CreateMap<Campaign, CampaignDto>()
                .ForMember(dest => dest.CampaignId, opt => opt.MapFrom(src => src.CampaignId))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CampaignName, opt => opt.MapFrom(src => src.CampaignName))
                .ForMember(dest => dest.UniversityId, opt => opt.MapFrom(src => src.UniversityId))
                .ForMember(dest => dest.UniversityName, opt => opt.MapFrom(src => src.University != null ? src.University.UniversityName : ""))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
                .ForMember(dest => dest.ModifyDate, opt => opt.MapFrom(src => src.ModifyDate));

        }
    }
}
