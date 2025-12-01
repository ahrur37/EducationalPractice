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
            }
        }
        else
        {
            TeachersBtn.IsVisible = false;
            StudentsBtn.IsVisible = false;
        }
    }

    private async void Button_Click_Teachers(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainControl.Content = new TeachersList();
    }

    private async void Button_Click_Disciplines(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainControl.Content = new DisciplinesList();
    }
    private async void Button_Click_Exams(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainControl.Content = new ExamsList();
    }

    private async void Button_Click_Students(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainControl.Content = new StudentsList();
    }

    private void Exit_Click(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<AuthControl>();
    }
}
