using System;
using System.Collections.Generic;

namespace SWD.NextIntern.Repository.Entities;

public partial class Role
{
    public Guid RoleId { get; set; }

    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public DateTime? DeletedDate { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
