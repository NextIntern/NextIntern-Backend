using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.Common.Mappings;
using System.Text.Json.Serialization;

public class InternDto : IMapFrom<User>
{
    public Guid UserId { get; set; }

    public string Id { get; set; }

    public string Username { get; set; }

    public string Fullname { get; set; }

    public DateOnly? Dob { get; set; }

    public string Gender { get; set; }

    public string Telephone { get; set; }

    public string Email { get; set; }

    public string Address { get; set; }

    public User Mentor { get; set; }

    public Campaign Campaign { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    //public string RoleName { get; set; }

    public Role? Role { get; set; }

    [JsonIgnore]
    public DateTime? DeletedDate { get; set; }

    public string? ImgUrl { get; set; }

    public string UniversityName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, InternDto>()
        .ForMember(dest => dest.UniversityName, opt => opt.MapFrom(src => src.Campaign.University.UniversityName));
    }

    //public void Mapping(Profile profile)
    //{
    //    profile.CreateMap<User, InternDto>()
    //    .ForMember(dest => dest.UniversityName, opt => opt.MapFrom(src => new UniversityNameResolver(_universityRepository)));
    //}

    //private class UniversityNameResolver : IValueResolver<User, InternDto, string>
    //{
    //    private readonly IUniversityRepository _universityRepository;

    //    public UniversityNameResolver(IUniversityRepository universityRepository)
    //    {
    //        _universityRepository = universityRepository;
    //    }

    //    public string Resolve(User source, InternDto destination, string destMember, ResolutionContext context)
    //    {
    //        var uni = _universityRepository.GetUniversityNameById(source.UniversityId);
    //        return uni ?? string.Empty;
    //    }
    //}
}