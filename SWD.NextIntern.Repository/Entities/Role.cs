using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SWD.NextIntern.Repository.Entities;

public partial class Role
{
    public Guid RoleId { get; set; }

    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    [JsonIgnore]
    public DateTime? DeletedDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
