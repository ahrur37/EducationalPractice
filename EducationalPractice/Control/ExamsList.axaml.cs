using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using EducationalPractice.Data;
using EducationalPractice.Models;
using EducationalPractice.Views;
using Microsoft.EntityFrameworkCore;

namespace EducationalPractice.Control;

public partial class ExamsList : UserControl
{
    public ExamsList()
    {
        InitializeComponent(); 
        LoadData();
    }

    private void LoadData()
    {
        DataGridItems.ItemsSource = App.DbContext.Exams
            .Include(p => p.StudentRegNavigation)
            .Include(p => p.ExaminerTabNavigation)
            .Include(p => p.DisciplineCodeNavigation)
            .Include(p => p.IdClassroomNavigation)
            .ToList();
    }

    private async void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (VariableData.authUser.TabNumEmployeeNavigation == null ||
            VariableData.authUser.TabNumEmployeeNavigation.PositionEmp == "преподаватель") return;
        
        var selectedExam = DataGridItems.SelectedItem as Exam;
        if(selectedExam == null)return; 
        
        VariableData.selectExam = selectedExam;
        
        var parent = this.VisualRoot as Window;
        var addwinwExams = new CreateAndChangeExams();
        await addwinwExams.ShowDialog(parent);
        
        LoadData();
    }

    private async void ChangeGradeButton_Click(object? sender, RoutedEventArgs e)
    {
        var selectedExam = (sender as Button)?.DataContext as Exam;
        if(selectedExam == null)return; 
        
        VariableData.selectExam = selectedExam;
        
        var parent = this.VisualRoot as Window;
        var addwinwExam = new ChangeGrade();
        await addwinwExam.ShowDialog(parent);

        LoadData();
    }
    
    private void DeleteButton_Click(object? sender, RoutedEventArgs e)
    {        
        var button = sender as Button;
        var selectedExam = button?.DataContext as Exam;
        
        if (DataGridItems == null) return;
        
        VariableData.selectExam = selectedExam;
        
        App.DbContext.Exams.Remove(selectedExam);
        App.DbContext.SaveChanges();

        LoadData();
    }

    private async void AddButton_Click(object? sender, RoutedEventArgs e)
    {
        VariableData.selectUser = null;
        
        var parent = this.VisualRoot as Window;
        var addwinwUser = new CreateAndChangeExams();
        await addwinwUser.ShowDialog(parent);

        LoadData();
    }
}