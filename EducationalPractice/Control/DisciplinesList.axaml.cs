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

public partial class DisciplinesList : UserControl
{
    private List<Discipline> allDiscipline = new List<Discipline>();
    public DisciplinesList()
    {
        InitializeComponent(); 
        LoadData();
    }

    private void LoadData()
    {
        DataGridItems.ItemsSource = App.DbContext.Disciplines
            .Include(p => p.IdDepartNavigation)
            .ToList();
    }

    private async void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
    }
    
    private void DeleteButton_Click(object? sender, RoutedEventArgs e)
    {
    }

    private async void AddButton_Click(object? sender, RoutedEventArgs e)
    {
        VariableData.selectDiscipline = null;
        
        var parent = this.VisualRoot as Window;
        var addDiscipline = new CreateAndChangeDisciplines();
        await addDiscipline.ShowDialog(parent);
    }
    
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