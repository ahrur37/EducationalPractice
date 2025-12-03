using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using EducationalPractice.Data;
using EducationalPractice.Models;

namespace EducationalPractice.Views;

public partial class CreateAndChangeStudents : Window
{
    public CreateAndChangeStudents()
    {
        InitializeComponent();
        
        ComboSpeciality.ItemsSource = App.DbContext.Specialties.ToList();

        if (VariableData.selectDiscipline != null)
        {
            ComboSpeciality.SelectedItem = VariableData.selectUser.RegNumberNavigation.IdSpeciality;
        }
        
        if (VariableData.selectUser == null)
        {
            DataContext = new Login();
        }
        
        DataContext = VariableData.selectUser;
    }
    private void SaveButton(object? sender, RoutedEventArgs e)
    {
        var selectedDepart = ComboSpeciality.SelectedItem as Specialty;
        
        if(string.IsNullOrEmpty(FullnameText.Text) || string.IsNullOrEmpty(LoginText.Text) || 
           ComboSpeciality.SelectedItem == null || string.IsNullOrEmpty(LoginText.Text) || 
           string.IsNullOrEmpty(PasswordText.Text)) return;
        
        var StudentDataContext = DataContext as Employee;
        StudentDataContext.IdDepart = selectedDepart.IdDepart;
        
        if (VariableData.selectUser == null)
        {
            App.DbContext.Employees.Add(StudentDataContext);
        }
        else
        {
            App.DbContext.Update(StudentDataContext);
        }
        
        App.DbContext.SaveChanges();
        this.Close();
    }
}