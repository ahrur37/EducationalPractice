using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

public partial class HeadOfDeptList : UserControl
{
    private List<HeadOfDept> _allHeads = new();
    private List<Department> _departments = new();
    
    public HeadOfDeptList()
    {
        InitializeComponent(); 
        LoadAllData();
    }

    private async void LoadAllData()
    {
        var heads = await App.DbContext.HeadOfDepts
            .Include(h => h.TabNumberNavigation)
            .ThenInclude(e => e.IdDepartNavigation)
            .ToListAsync();

        _allHeads = heads;
        HeadDataGrid.ItemsSource = _allHeads;

        // Загружаем кафедры для фильтра
        _departments = await App.DbContext.Departments.ToListAsync();
        DepartmentFilter.Items.Clear();
        DepartmentFilter.Items.Add(new ComboBoxItem { Content = "Все кафедры", Tag = "" });
        foreach (var dept in _departments)
        {
            DepartmentFilter.Items.Add(new ComboBoxItem
            {
                Content = dept.NameDepart,
                Tag = dept.IdDepart
            });
        }

        ApplyFilter();
    }

    private async void AddHeadButton_Click(object? sender, RoutedEventArgs e)
    {
        VariableData.selectUser = null;

        var parent = this.VisualRoot as Window;
        var addnewTeacher = new CreateAndChangeDisciplines();

        await addnewTeacher.ShowDialog(parent);
    }

    private void DeleteHeadButton_Click(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void ApplyFilter()
    {
        var search = SearchBox.Text?.ToLower() ?? "";
        var selectedDept = DepartmentFilter.SelectedItem as ComboBoxItem;
        var deptId = selectedDept?.Tag as int?;

        var filtered = _allHeads.Where(h =>
        {
            bool matchesSearch = string.IsNullOrEmpty(search) ||
                                 (h.TabNumberNavigation?.Fullname?.ToLower().Contains(search) == true);
            bool matchesDept = !deptId.HasValue ||
                               h.TabNumberNavigation?.IdDepart == deptId.Value;
            return matchesSearch && matchesDept;
        }).ToList();

        HeadDataGrid.ItemsSource = filtered;
    }

    private void SearchBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        ApplyFilter();
    }

    private void DepartmentFilter_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        ApplyFilter();
    }
}