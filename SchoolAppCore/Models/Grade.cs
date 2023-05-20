using System;
using System.Collections.Generic;

namespace SchoolAppCore.Models;

public partial class Grade
{
    public int GradeId { get; set; }

    public int? GradeVal { get; set; }

    public int? StudentId { get; set; }

    public int? SubjectId { get; set; }

    public virtual Student? Student { get; set; }

    public virtual Subject? Subject { get; set; }
}
