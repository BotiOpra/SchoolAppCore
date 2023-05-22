using System;
using System.Collections.Generic;

namespace SchoolAppCore.Models;

public partial class HeadTeacher
{
    public int HeadTeacherId { get; set; }

    public string? Email { get; set; }

    public int? ClassId { get; set; }

    public virtual Class? Class { get; set; }

    public virtual User? EmailNavigation { get; set; }
}
