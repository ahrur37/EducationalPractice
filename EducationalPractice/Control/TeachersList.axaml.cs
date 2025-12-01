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
    }

    private void ComboCategory_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        ApplyAllFilter();
    }

    private void ApplyFilter()
    {
    }
    
    // private IEnumerable<Teacher> ApplyPriceFilter(IEnumerable<Teacher> products)
    // {
    // }
    
    private void ApplyAllFilter()
    {
    }

    private void MinPrice_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        ApplyAllFilter();
    }

    private void MaxPrice_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        ApplyAllFilter();
    }

    private void ResetButton_Click(object? sender, RoutedEventArgs e)
    {
    }
}