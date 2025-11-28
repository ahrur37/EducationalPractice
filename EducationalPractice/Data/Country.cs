using System;
using System.Collections.Generic;

namespace EducationalPractice.Data;

public partial class Country
{
    public int IdCountries { get; set; }

    public string? Title { get; set; }

    public string? Capital { get; set; }

    public int? Square { get; set; }

    public int? Population { get; set; }

    public string? Continent { get; set; }
}
