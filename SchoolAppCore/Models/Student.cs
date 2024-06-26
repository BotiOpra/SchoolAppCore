﻿using System;
using System.Collections.Generic;

namespace SchoolAppCore.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? ClassId { get; set; }

    public virtual ICollection<Absence> Absences { get; set; } = new List<Absence>();

    public virtual Class? Class { get; set; }

    public virtual User? EmailNavigation { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
