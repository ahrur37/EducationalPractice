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

        if (VariableData.selectUser == null)
        {
            DataContext = new Login(
                new Employee());
        }

        if (VariableData.selectedProdCategory != null)
        {
            ComboDepart.SelectedItem = VariableData.selectUser.TabNumEmployeeNavigation.IdDepartNavigation;
        }
        DataContext = VariableData.selectUser;
    }
    private void SaveButton(object? sender, RoutedEventArgs e)
    {
        
    }
}