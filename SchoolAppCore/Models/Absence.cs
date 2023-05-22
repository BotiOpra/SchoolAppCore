using System;
using System.Collections.Generic;

namespace SchoolAppCore.Models;

public partial class Absence
{
    public int AbsenceId { get; set; }

    public DateTime? AbsenceDate { get; set; }

    public int? StudentId { get; set; }

    public int? SubjectId { get; set; }

    public virtual Student? Student { get; set; }

    public virtual Subject? Subject { get; set; }
}
