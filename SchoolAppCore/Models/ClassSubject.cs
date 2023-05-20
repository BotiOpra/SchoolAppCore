using System;
using System.Collections.Generic;

namespace SchoolAppCore.Models;

public partial class ClassSubject
{
    public int ClassId { get; set; }

    public int SubjectId { get; set; }

    public bool HasSemesterPaper { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
