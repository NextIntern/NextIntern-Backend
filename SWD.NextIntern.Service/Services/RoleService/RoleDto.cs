using AutoMapper;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Service.Common.Mappings;
using System.Text.Json.Serialization;

namespace SWD.NextIntern.Service.Services.RoleService
{
    public class RoleDto : IMapFrom<Role>
    {
        public Guid RoleId { get; set; }

        //public string Id { get; set; }

        public string RoleName { get; set; }

        [JsonIgnore]
        public DateTime? DeletedDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Role, RoleDto>();
        }
    }
}
