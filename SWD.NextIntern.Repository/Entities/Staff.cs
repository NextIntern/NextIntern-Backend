using System;
using System.Collections.Generic;

namespace NextIntern.Domain.Entities;

public partial class Staff
{
    public Guid StaffId { get; set; }

    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public Guid? RoleId { get; set; }

    public virtual ICollection<Intern> Interns { get; set; } = new List<Intern>();

    public virtual Role? Role { get; set; }
}
