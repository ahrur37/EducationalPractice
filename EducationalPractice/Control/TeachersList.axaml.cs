using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using EducationalPractice.Models;
using EducationalPractice.Data;
using EducationalPractice.Views;
using Microsoft.EntityFrameworkCore;

namespace EducationalPractice.Control;

public partial class TeachersList : UserControl
{
    private List<Teacher> allTeachers = new List<Teacher>();
    public TeachersList()
    {
        InitializeComponent(); 
        LoadData();
    }

    private void LoadData()
    {
        DataGridTeachers.ItemsSource = App.DbContext.Teachers
            .Include(p => p.TabNumberNavigation)
            .ToList();
    }

    private async void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        var selectedUser = DataGridTeachers.SelectedItem as Login;
        if(selectedUser == null)return;
        
        VariableData.selectUser = selectedUser;
        
        var parent = this.VisualRoot as Window;
        var addwinwUser = new CreateAndChangeTeachers();
        await addwinwUser.ShowDialog(parent);

        LoadData();
    }
    
    private async void AddButton_Click(object? sender, RoutedEventArgs e)
    {
        VariableData.selectUser = null;
            
        var parent = this.VisualRoot as Window;
        var addwinwTeacher = new CreateAndChangeTeachers();
        await addwinwTeacher.ShowDialog(parent);
    }
    
    private void DeleteButton_Click(object? sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var selectUser = button?.DataContext as Login;
        
        if (DataGridTeachers == null) return;
        
        VariableData.selectUser = selectUser;
        
        App.DbContext.Logins.Remove(selectUser);
        App.DbContext.SaveChanges();
        
        DataGridTeachers.ItemsSource = App.DbContext.Exams.ToList();
    }
}
