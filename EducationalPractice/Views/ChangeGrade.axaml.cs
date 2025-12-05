using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using EducationalPractice.Data;
using EducationalPractice.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationalPractice.Views;

public partial class ChangeGrade : Window
{
    private Exam _exam;
    public ChangeGrade()
    {
        InitializeComponent();
        if (VariableData.selectExam == null)
        {
            Close();
            return;
        }

        _exam = App.DbContext.Exams
            .Include(e => e.IdClassroomNavigation)
            .Include(e => e.ExaminerTabNavigation)
            .Include(e => e.StudentRegNavigation)
            .Include(e => e.DisciplineCodeNavigation)
            .First(e => e.IdExam == VariableData.selectExam.IdExam);

        DataContext = _exam;

        // Заполняем оценки
        ComboGrade.Items.Clear();
        foreach (int grade in new[] { 2, 3, 4, 5 })
        {
            ComboGrade.Items.Add(new ComboBoxItem { Content = grade.ToString() });
        }

        // Выбираем текущую оценку
        if (_exam.Grade is int currentGrade)
        {
            foreach (ComboBoxItem item in ComboGrade.Items)
            {
                if (item.Content?.ToString() == currentGrade.ToString())
                {
                    ComboGrade.SelectedItem = item;
                    break;
                }
            }
        }
    }

    private void Save_Click(object? sender, RoutedEventArgs e)
    {
        var selectedItem = ComboGrade.SelectedItem as ComboBoxItem;
        string? gradeText = selectedItem?.Content?.ToString();

        // Проверяем и парсим оценку
        if (string.IsNullOrEmpty(gradeText) || !int.TryParse(gradeText, out int grade))
        {
            return; // или показать ошибку
        }

        _exam.Grade = grade; // присваиваем int

        App.DbContext.Update(_exam);
        App.DbContext.SaveChanges();
        this.Close();
    }
}