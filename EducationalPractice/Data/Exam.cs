using System;
using System.Collections.Generic;

namespace EducationalPractice.Data;

public partial class Exam
{
    public int IdExam { get; set; }

    public DateOnly? ExamDate { get; set; }

    public int? DisciplineCode { get; set; }

    public int? StudentReg { get; set; }

    public int? ExaminerTab { get; set; }

    public int? Grade { get; set; }

    public int? IdClassroom { get; set; }

    public virtual Discipline? DisciplineCodeNavigation { get; set; }

    public virtual Employee? ExaminerTabNavigation { get; set; }

    public virtual Classroom? IdClassroomNavigation { get; set; }

    public virtual Student? StudentRegNavigation { get; set; }
}
