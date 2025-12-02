using System;
using System.Collections.Generic;

namespace EducationalPractice.Data;

public partial class Classroom
{
    public int IdClassroom { get; set; }

    public string? ClassRoom { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
