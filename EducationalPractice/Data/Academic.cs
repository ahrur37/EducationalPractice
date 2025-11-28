using System;
using System.Collections.Generic;

namespace EducationalPractice.Data;

public partial class Academic
{
    public int IdAcademics { get; set; }

    public string? Fname { get; set; }

    public DateOnly? Birthdate { get; set; }

    public string? Specialization { get; set; }

    public int? YearAccumulation { get; set; }
}
