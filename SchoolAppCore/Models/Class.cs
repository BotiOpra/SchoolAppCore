using System;
using System.Collections.Generic;

namespace SchoolAppCore.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public int? ClassYear { get; set; }

    public int? SpecId { get; set; }

    public virtual ICollection<ClassSubject> ClassSubjects { get; set; } = new List<ClassSubject>();

    public virtual Professor? Professor { get; set; }

    public virtual Specialization? Spec { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
