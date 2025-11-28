using System;
using System.Collections.Generic;

namespace EducationalPractice.Data;

public partial class Login
{
    public int IdLogin { get; set; }

    public int? TabNumEmployee { get; set; }

    public string? Login1 { get; set; }

    public string? Password { get; set; }

    public virtual Employee? TabNumEmployeeNavigation { get; set; }
}
