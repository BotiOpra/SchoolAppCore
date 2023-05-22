﻿using System;
using System.Collections.Generic;

namespace SchoolAppCore.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string? SubjectName { get; set; }

    public int? ProfId { get; set; }

    public virtual ICollection<ClassSubject> ClassSubjects { get; set; } = new List<ClassSubject>();

    public virtual Professor? Prof { get; set; }
}
