using System;
using System.Collections.Generic;

namespace SWD.NextIntern.Repository.Entities;

public partial class Role
{
    public Guid RoleId { get; set; }

    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public DateTime? DeletedDate { get; set; }

    public virtual ICollection<Intern> Interns { get; set; } = new List<Intern>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
