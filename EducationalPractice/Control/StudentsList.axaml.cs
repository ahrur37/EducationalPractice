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
using MsBox.Avalonia;

namespace EducationalPractice.Control;

public partial class StudentsList : UserControl
{
    private List<Student> _allStudents = new();
    private List<Specialty> _specialities = new();
    
    public StudentsList()
    {
        InitializeComponent();
        LoadData();
    }

    private void LoadData()
    {
        // Загрузка всех студентов и специальностей
        _allStudents = App.DbContext.Students
            .Include(s => s.IdSpecialityNavigation)
            .ToList();

        _specialities = App.DbContext.Specialties.ToList();

        // Заполнение ComboBox
        SpecialityFilter.ItemsSource = _specialities.Select(s => s.Direction).ToList(); // или s.Code, если нужно по коду

        // Применение фильтров (изначально без фильтрации)
        ApplyFilters();
    }

    private void ApplyFilters()
    {
        var search = SearchBox.Text?.ToLower() ?? "";
        var selectedSpeciality = SpecialityFilter.SelectedItem as string;

        var filtered = _allStudents.Where(s =>
        {
            // Проверка по ФИО студента
            bool matchesSearch = string.IsNullOrEmpty(search) ||
                                 s.Fullname?.ToLower().Contains(search) == true;

            // Проверка по специальности
            bool matchesSpeciality = string.IsNullOrEmpty(selectedSpeciality) ||
                                     s.IdSpecialityNavigation?.Direction == selectedSpeciality;

            return matchesSearch && matchesSpeciality;
        }).ToList();

        DataGridItems.ItemsSource = filtered;
    }

    private void SearchBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        ApplyFilters();
    }

    private void SpecialityFilter_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        ApplyFilters();
    }

    private async void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (VariableData.authUser.TabNumEmployeeNavigation.PositionEmp == "преподаватель") 
        {
            await MessageBoxManager.GetMessageBoxStandard("Ошибка", "У вас не хватает прав").ShowAsync();
            return;
        }

        var selectedStudent = DataGridItems.SelectedItem as Student; // Исправлено: Student, а не Login
        if(selectedStudent == null) return; 
        
        VariableData.selectStudent = selectedStudent; // Исправлено: предполагаем, что есть VariableData.selectStudent
        
        var parent = this.VisualRoot as Window;
        var addwinwStudents = new CreateAndChangeStudents(); // Исправлено: предполагаем, что есть CreateAndChangeStudents
        await addwinwStudents.ShowDialog(parent);
        
        LoadData();
    }
    
    private async void DeleteButton_Click(object? sender, RoutedEventArgs e)
    {        
        if (VariableData.authUser.TabNumEmployeeNavigation.PositionEmp == "преподаватель") 
        {
            await MessageBoxManager.GetMessageBoxStandard("Ошибка", "У вас не хватает прав").ShowAsync();
            return;
        }
        
        var button = sender as Button;
        var selectedStudent = button?.DataContext as Student; // Исправлено: Student, а не Login
        
        if (selectedStudent == null) return;
        
        VariableData.selectStudent = selectedStudent; // Исправлено
        
        App.DbContext.Students.Remove(selectedStudent); // Исправлено: Students, а не Logins
        App.DbContext.SaveChanges();
        
        LoadData(); // Обновляем список
    }

    private async void AddButton_Click(object? sender, RoutedEventArgs e)
    {
        if (VariableData.authUser.TabNumEmployeeNavigation.PositionEmp == "преподаватель") 
        {
            await MessageBoxManager.GetMessageBoxStandard("Ошибка", "У вас не хватает прав").ShowAsync();
            return;
        }

        VariableData.selectStudent = null; // Исправлено

        var parent = this.VisualRoot as Window;
        var addwinwUser = new CreateAndChangeStudents(); // Исправлено
        await addwinwUser.ShowDialog(parent);
        
        LoadData();
    }
}