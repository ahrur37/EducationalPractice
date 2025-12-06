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
using MsBox.Avalonia;

namespace EducationalPractice.Control;

public partial class DisciplinesList : UserControl
{
    private List<Discipline> _allDisciplines = new(); // Переименовал для ясности
    private List<Department> _departments = new();
    
    public DisciplinesList()
    {
        InitializeComponent(); 
        LoadData();
    }

    private async void LoadData()
    {
        // Загрузка всех дисциплин и департаментов
        _allDisciplines = App.DbContext.Disciplines
            .Include(p => p.IdDepartNavigation)
            .ToList();

        _departments = App.DbContext.Departments.ToList();

        // Заполнение ComboBox
        DepartmentFilter.ItemsSource = _departments.Select(d => d.NameDepart).ToList();

        // Применение фильтров (изначально без фильтрации)
        ApplyFilters();
    }

    private void ApplyFilters()
    {
        var search = SearchBox.Text?.ToLower() ?? "";
        var selectedDeptName = DepartmentFilter.SelectedItem as string;

        var filtered = _allDisciplines.Where(d =>
        {
            // Проверка по названию дисциплины
            bool matchesSearch = string.IsNullOrEmpty(search) ||
                                 d.NameDisc?.ToLower().Contains(search) == true;

            // Проверка по кафедре
            bool matchesDept = string.IsNullOrEmpty(selectedDeptName) ||
                               d.IdDepartNavigation?.NameDepart == selectedDeptName;

            return matchesSearch && matchesDept;
        }).ToList();

        DataGridItems.ItemsSource = filtered;
    }

    private void SearchBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        ApplyFilters();
    }

    private void DepartmentFilter_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        ApplyFilters();
    }

    private async void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {       
        if (VariableData.authUser.TabNumEmployeeNavigation == null ||
            VariableData.authUser.TabNumEmployeeNavigation.PositionEmp == "преподаватель") 
        {
            await MessageBoxManager.GetMessageBoxStandard("Ошибка", "У вас не хватает прав").ShowAsync();
            return;
        }

        var selectedDiscipline = DataGridItems.SelectedItem as Discipline;
        if(selectedDiscipline == null) return; 
        
        VariableData.selectDiscipline = selectedDiscipline;
        
        var parent = this.VisualRoot as Window;
        var addwinwDisciplines = new CreateAndChangeDisciplines();
        await addwinwDisciplines.ShowDialog(parent);
        
        LoadData(); // Обновляем после редактирования
    }
    
    private async void DeleteButton_Click(object? sender, RoutedEventArgs e)
    {
        if (VariableData.authUser.TabNumEmployeeNavigation == null ||
            VariableData.authUser.TabNumEmployeeNavigation.PositionEmp == "преподаватель") 
        {
            await MessageBoxManager.GetMessageBoxStandard("Ошибка", "У вас не хватает прав").ShowAsync();
            return;
        }

        var button = sender as Button;
        var selectDiscipline = button?.DataContext as Discipline;
        
        if (selectDiscipline == null) return;
        
        VariableData.selectDiscipline = selectDiscipline;
        
        App.DbContext.Disciplines.Remove(selectDiscipline);
        App.DbContext.SaveChanges();
        
        LoadData(); // Обновляем список
    }

    private async void AddButton_Click(object? sender, RoutedEventArgs e)
    {
        if (VariableData.authUser.TabNumEmployeeNavigation == null ||
            VariableData.authUser.TabNumEmployeeNavigation.PositionEmp == "преподаватель") 
        {
            await MessageBoxManager.GetMessageBoxStandard("Ошибка", "У вас не хватает прав").ShowAsync();
            return;
        }

        VariableData.selectDiscipline = null;
        
        var parent = this.VisualRoot as Window;
        var addDiscipline = new CreateAndChangeDisciplines();
        await addDiscipline.ShowDialog(parent);
        
        LoadData(); // Обновляем список
    }
}