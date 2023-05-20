using System;
using System.Collections.Generic;

namespace SchoolAppCore.Models;

public partial class Specialization
{
    public int SpecId { get; set; }

    public string? SpecName { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
