using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using EducationalPractice.Data;
using EducationalPractice.Models;

namespace EducationalPractice.Views;

public partial class CreateAndChangeExams : Window
{
    public CreateAndChangeExams()
    {
        InitializeComponent(); 
        DataContext = VariableData.selectDiscipline;
        
        ComboClassroom.ItemsSource = App.DbContext.Classrooms.ToList();
        ComboExaminer.ItemsSource = App.DbContext.Employees.Where(e => e.PositionEmp == "преподаватель" 
                                                                       || e.PositionEmp == "зав. кафедрой").ToList();
        ComboStudent.ItemsSource = App.DbContext.Students.ToList();
        ComboDiscipline.ItemsSource = App.DbContext.Disciplines.ToList();
    
        if (VariableData.selectExam != null)
        {
            ComboClassroom.SelectedItem = VariableData.selectExam.IdClassroomNavigation;
            ComboExaminer.SelectedItem = VariableData.selectExam.ExaminerTabNavigation;
            ComboStudent.SelectedItem = VariableData.selectExam.StudentRegNavigation;
            ComboDiscipline.SelectedItem = VariableData.selectExam.DisciplineCodeNavigation;
        }
        
        if (VariableData.selectDiscipline == null)
        {
            DataContext = new Discipline();
        }
    }
    
    private void SaveButton(object? sender, RoutedEventArgs e)
    {
        var selectedClassroom = ComboClassroom.SelectedItem as Classroom;
        var selectedExaminer = ComboExaminer.SelectedItem as Employee;
        var selectedStudent = ComboStudent.SelectedItem as Student;
        var selectedDiscipline = ComboDiscipline.SelectedItem as Discipline;
            
        if(string.IsNullOrEmpty(ExamDatePicker.DayFormat) || string.IsNullOrEmpty(DisciplineCodeText.Text) || 
           ComboClassroom.SelectedItem == null || ComboExaminer.SelectedItem == null ||
           ComboStudent.SelectedItem == null || ComboDiscipline.SelectedItem == null) return;
            
        var disciplineDataContext = DataContext as Exam;
        disciplineDataContext.DisciplineCode = selectedClassroom.IdClassroom;
        disciplineDataContext.DisciplineCode = selectedExaminer.TabNumEmployee;
        disciplineDataContext.DisciplineCode = selectedStudent.RegNumber;
        disciplineDataContext.DisciplineCode = selectedDiscipline.IdDiscipline;
        
        if (VariableData.selectExam == null)
        {
            App.DbContext.Exams.Add(disciplineDataContext);
        }
        else
        {
            App.DbContext.Update(disciplineDataContext);
        }
            
        App.DbContext.SaveChanges();
        this.Close();
    }
}