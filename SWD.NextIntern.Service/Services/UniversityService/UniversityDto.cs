using AutoMapper;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Service.Common.Mappings;

public class UniversityDto : IMapFrom<University>
{
    public Guid UniversityId { get; set; }

    public string UniversityName { get; set; } = null!;

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<University, UniversityDto>();
    }
}