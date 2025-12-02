using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using EducationalPractice.Data;
using EducationalPractice.Models;

namespace EducationalPractice.Views;

public partial class CreateAndChangeTeachers : Window
{
    public CreateAndChangeTeachers()
    {
        InitializeComponent();
        
        ComboDepart.ItemsSource = App.DbContext.Departments.ToList();
        ComboChief.ItemsSource = App.DbContext.Employees.ToList();

        if (VariableData.selectDiscipline != null)
        {
            ComboDepart.SelectedItem = VariableData.selectUser.TabNumEmployeeNavigation.IdDepartNavigation;
        }
        
        if (VariableData.selectUser == null)
        {
            DataContext = new Login();
        }
        
        DataContext = VariableData.selectUser;
    }
    private void SaveButton(object? sender, RoutedEventArgs e)
    {
        var selectedDepart = ComboDepart.SelectedItem as Department;
        var selectedChief = ComboChief.SelectedItem as Employee;
        
        if(string.IsNullOrEmpty(FNameText.Text) || string.IsNullOrEmpty(RankText.Text) || 
           string.IsNullOrEmpty(DegreeText.Text) || string.IsNullOrEmpty(SalaryText.Text) || 
           ComboDepart.SelectedItem == null || string.IsNullOrEmpty(LoginText.Text) || 
           string.IsNullOrEmpty(PasswordText.Text) || ComboChief.SelectedItem == null) return;
        
        var TeacherDataContext = DataContext as Employee;
        TeacherDataContext.IdDepart = selectedDepart.IdDepart;
        TeacherDataContext.Chief = selectedChief.TabNumEmployee;
        
        if (VariableData.selectDiscipline == null)
        {
            App.DbContext.Employees.Add(TeacherDataContext);
        }
        else
        {
            App.DbContext.Update(TeacherDataContext);
        }
        
        App.DbContext.SaveChanges();
        this.Close();
    }
}