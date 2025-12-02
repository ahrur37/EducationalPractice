using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using EducationalPractice.Data;
using EducationalPractice.Models;

namespace EducationalPractice.Views;

public partial class CreateAndChangeDisciplines : Window
{
    public CreateAndChangeDisciplines()
    {
        InitializeComponent();
        DataContext = VariableData.selectDiscipline;
        
        ComboDepart.ItemsSource = App.DbContext.Departments.ToList();

        if (VariableData.selectDiscipline != null)
        {
            ComboDepart.SelectedItem = VariableData.selectDiscipline.IdDepartNavigation;
        }
        
        if (VariableData.selectDiscipline == null)
        {
            DataContext = new Discipline();
        }
    }
    
    private void SaveButton(object? sender, RoutedEventArgs e)
    {
        var selectedDepart = ComboDepart.SelectedItem as Department;
        
        if(string.IsNullOrEmpty(CodeText.Text) || string.IsNullOrEmpty(HoursText.Text) || 
           string.IsNullOrEmpty(NameDiscText.Text) || ComboDepart.SelectedItem == null) return;
        
        var disciplineDataContext = DataContext as Discipline;
        disciplineDataContext.IdDepart = selectedDepart.IdDepart;
        
        if (VariableData.selectDiscipline == null)
        {
            App.DbContext.Disciplines.Add(disciplineDataContext);
        }
        else
        {
            App.DbContext.Update(disciplineDataContext);
        }
        
        App.DbContext.SaveChanges();
        this.Close();
    }
}