using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using EducationalPractice.Models;

namespace EducationalPractice.Control;

public partial class MainControls : UserControl
{
    public MainControls()
    {
        InitializeComponent();
        MainControl.Content = new ExamsList();

        if (VariableData.authUser.TabNumEmployeeNavigation != null)
        {
            if (VariableData.authUser.TabNumEmployeeNavigation.PositionEmp == "преподаватель")
            {
                TeachersBtn.IsVisible = false;
                HeadOfDeptBtn.IsVisible = false;
            }

            if (VariableData.authUser.TabNumEmployeeNavigation.PositionEmp == "зав. кафедрой")
            {
                HeadOfDeptBtn.IsVisible = false;
            }
        }
        else
        {
            TeachersBtn.IsVisible = false;
            StudentsBtn.IsVisible = false;
            HeadOfDeptBtn.IsVisible = false;
        }
    }

    private void Button_Click_Teachers(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainControl.Content = new TeachersList();
    }

    private void Button_Click_Disciplines(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainControl.Content = new DisciplinesList();
    }
    private void Button_Click_Exams(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainControl.Content = new ExamsList();
    }

    private void Button_Click_Students(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainControl.Content = new StudentsList();
    }

    private void Exit_Click(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<AuthControl>();
    }
    
    private void Button_Click_HeadOfDeptBtn(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainControl.Content = new HeadOfDeptList();
    }
}
