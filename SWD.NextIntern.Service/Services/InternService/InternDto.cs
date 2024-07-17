using AutoMapper;
using SWD.NextIntern.Repository.Entities;
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

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, InternDto>();
    }
}