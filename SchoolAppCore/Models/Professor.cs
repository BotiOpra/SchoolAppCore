using System;
using System.Collections.Generic;

namespace SchoolAppCore.Models;

public partial class Professor
{
    public int ProfId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int? ClassId { get; set; }

    public virtual Class? Class { get; set; }

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
