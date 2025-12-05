using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using EducationalPractice.Data;
using EducationalPractice.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationalPractice.Views;

public partial class CreateAndChangeExams : Window
{
    private Exam _exam;
    
    public CreateAndChangeExams()
    {
        InitializeComponent(); 

        // Заполняем списки
        ComboClassroom.ItemsSource = App.DbContext.Classrooms.ToList();
        ComboExaminer.ItemsSource = App.DbContext.Employees.Where(e => e.PositionEmp == "преподаватель" 
                        || e.PositionEmp == "зав. кафедрой").ToList();
        ComboStudent.ItemsSource = App.DbContext.Students.ToList();
        ComboDiscipline.ItemsSource = App.DbContext.Disciplines.ToList();

        if (VariableData.selectExam != null)
        {
            _exam = App.DbContext.Exams
                .Include(e => e.IdClassroomNavigation)
                .Include(e => e.ExaminerTabNavigation)
                .Include(e => e.StudentRegNavigation)
                .Include(e => e.DisciplineCodeNavigation)
                .First(e => e.IdExam == VariableData.selectExam.IdExam);

            ExamDatePicker.SelectedDate =  DateTimeOffset.Now; 
        }
        else
        {
            _exam = new Exam();
            ExamDatePicker.SelectedDate = DateTime.Today; // ✅ DateTime, не DateTimeOffset!
        }

        DataContext = _exam;

        if (VariableData.selectExam != null)
        {
            ComboClassroom.SelectedItem = _exam.IdClassroomNavigation;
            ComboExaminer.SelectedItem = _exam.ExaminerTabNavigation;
            ComboStudent.SelectedItem = _exam.StudentRegNavigation;
            ComboDiscipline.SelectedItem = _exam.DisciplineCodeNavigation;
        }
    }


    private void SaveButton(object? sender, RoutedEventArgs e)
    {
        var selectedClassroom = ComboClassroom.SelectedItem as Classroom;
        var selectedExaminer = ComboExaminer.SelectedItem as Employee;
        var selectedStudent = ComboStudent.SelectedItem as Student;
        var selectedDiscipline = ComboDiscipline.SelectedItem as Discipline;
        var selectedDate = ExamDatePicker.SelectedDate.Value;
        var dateOnly = new DateOnly(selectedDate.Year, selectedDate.Month, selectedDate.Day);
        
        if(string.IsNullOrEmpty(ExamDatePicker.DayFormat) || 
           ComboClassroom.SelectedItem == null || ComboExaminer.SelectedItem == null ||
           ComboStudent.SelectedItem == null || ComboDiscipline.SelectedItem == null) return;
            
        var disciplineDataContext = DataContext as Exam;

        _exam.ExamDate = dateOnly;
        _exam.IdClassroom = selectedClassroom.IdClassroom;
        _exam.ExaminerTab = selectedExaminer.TabNumEmployee;
        _exam.StudentReg = selectedStudent.RegNumber;
        _exam.DisciplineCode = selectedDiscipline.IdDiscipline;

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