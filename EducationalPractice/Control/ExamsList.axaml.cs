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
    private List<Exam> allTeachers = new List<Exam>();
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
            .ToList();
    }

    private async void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
    }

    private void BasketAdd_Click(object? sender, RoutedEventArgs e)
    {
    }

    private void DeleteButton_Click(object? sender, RoutedEventArgs e)
    {
    }

    private async void AddButton_Click(object? sender, RoutedEventArgs e)
    {
        VariableData.selectUser = null;
        
        var parent = this.VisualRoot as Window;
        var addwinwUser = new CreateAndChangeTeachers();
        await addwinwUser.ShowDialog(parent);
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