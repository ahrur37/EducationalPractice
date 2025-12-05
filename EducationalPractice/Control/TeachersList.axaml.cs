using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using EducationalPractice.Data;
using EducationalPractice.Models;
using EducationalPractice.Views;
using Microsoft.EntityFrameworkCore;

namespace EducationalPractice.Control;

public partial class TeachersList : UserControl
{
    private List<Teacher> _allTeachers = new();
    private string _searchTerm = "";
    private decimal? _minSalary = null;
    private decimal? _maxSalary = null;
    private int _sortMode = 0; // 0=по умолчанию (ФИО), 1=ФИО↑, 2=ЗП↑, 3=ЗП↓

    public TeachersList()
    {
        InitializeComponent();
        LoadData();
    }

    private void LoadData()
    {
        _allTeachers = App.DbContext.Teachers
            .Include(t => t.TabNumberNavigation)
            .ToList();

        ApplyFilterAndSort();
    }

    private async void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        var selectedUser = DataGridTeachers.SelectedItem as Login;
        if(selectedUser == null) return;
        
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
        
        if (selectUser == null) return;
        
        VariableData.selectUser = selectUser;
        
        App.DbContext.Logins.Remove(selectUser);
        App.DbContext.SaveChanges();
        
        LoadData(); // Обновляем список после удаления
    }

    // Извлечение фамилии
    private static string ExtractLastName(string fullname)
    {
        return string.IsNullOrWhiteSpace(fullname)
            ? ""
            : fullname.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];
    }

    // Обработчики фильтров
    private void SearchBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        var textBox = sender as TextBox;
        _searchTerm = (textBox?.Text ?? "").Trim().ToLower();
        ApplyFilterAndSort();
    }

    private void MinSalary_TextChanged(object? sender, TextChangedEventArgs e)
    {
        if (decimal.TryParse((sender as TextBox)?.Text, out decimal value))
            _minSalary = value;
        else
            _minSalary = null;

        ApplyFilterAndSort();
    }

    private void MaxSalary_TextChanged(object? sender, TextChangedEventArgs e)
    {
        if (decimal.TryParse((sender as TextBox)?.Text, out decimal value))
            _maxSalary = value;
        else
            _maxSalary = null;

        ApplyFilterAndSort();
    }

    // Обработчики сортировки
    private void SortByFullName_Click(object? sender, RoutedEventArgs e)
    {
        _sortMode = 1;
        ApplyFilterAndSort();
    }

    private void SortBySalaryAsc_Click(object? sender, RoutedEventArgs e)
    {
        _sortMode = 2;
        ApplyFilterAndSort();
    }

    private void SortBySalaryDesc_Click(object? sender, RoutedEventArgs e)
    {
        _sortMode = 3;
        ApplyFilterAndSort();
    }
    
    private void ApplyFilterAndSort()
    {
        var filtered = _allTeachers.AsEnumerable();

        // 🔍 Фильтр по ФИО
        if (!string.IsNullOrEmpty(_searchTerm))
        {
            filtered = filtered.Where(t =>
                ExtractLastName(t.TabNumberNavigation?.Fullname)
                    .ToLower()
                    .Contains(_searchTerm)
            );
        }

        // 💰 Фильтр по зарплате
        if (_minSalary.HasValue)
            filtered = filtered.Where(t => t.TabNumberNavigation?.Salary >= _minSalary);

        if (_maxSalary.HasValue)
            filtered = filtered.Where(t => t.TabNumberNavigation?.Salary <= _maxSalary);

        // 📅 Сортировка
        var sorted = _sortMode switch
        {
            1 => filtered.OrderBy(t => ExtractLastName(t.TabNumberNavigation?.Fullname)),
            2 => filtered.OrderBy(t => t.TabNumberNavigation?.Salary),
            3 => filtered.OrderByDescending(t => t.TabNumberNavigation?.Salary),
            _ => filtered.OrderBy(t => ExtractLastName(t.TabNumberNavigation?.Fullname)) // по умолчанию — по фамилии
        };

        DataGridTeachers.ItemsSource = sorted.ToList();
    }
}