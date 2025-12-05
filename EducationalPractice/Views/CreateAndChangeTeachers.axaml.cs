using System;
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
        ComboChief.ItemsSource = App.DbContext.Employees.ToList();

        if (VariableData.selectDiscipline != null)
        {
            ComboDepart.SelectedItem = VariableData.selectUser.TabNumEmployeeNavigation.IdDepartNavigation;
        }
        
        if (VariableData.selectUser == null)
        {
            DataContext = new Login()
            {
                TabNumEmployeeNavigation = new Employee()
                {
                    Teacher = new Teacher()
                }
            };
            return;
        }
        
        DataContext = VariableData.selectUser;
    }
    private async void SaveButton(object? sender, RoutedEventArgs e)
    {
        var selectedDepart = ComboDepart.SelectedItem as Department;
        var selectedChief = ComboChief.SelectedItem as Employee;

        // Проверяем обязательные поля
        if (string.IsNullOrWhiteSpace(FNameText.Text) ||
            string.IsNullOrWhiteSpace(RankText.Text) ||
            string.IsNullOrWhiteSpace(DegreeText.Text) ||
            string.IsNullOrWhiteSpace(SalaryText.Text) ||
            selectedDepart == null ||
            string.IsNullOrWhiteSpace(LoginText.Text) ||
            string.IsNullOrWhiteSpace(PasswordText.Text) ||
            selectedChief == null)
        {
            return;
        }

        // Предположим, что основной сущностью является Employee
        var employee = DataContext as Login;

        if (employee == null)
        {
            return;
        }

        // Обновляем данные из формы
        employee.TabNumEmployeeNavigation.Fullname = FNameText.Text.Trim();
        employee.TabNumEmployeeNavigation.Teacher.Rank = RankText.Text.Trim();
        employee.TabNumEmployeeNavigation.Teacher.Degree = DegreeText.Text.Trim();
        employee.TabNumEmployeeNavigation.Salary = decimal.Parse(SalaryText.Text); // ← добавьте try/catch при необходимости
        employee.Login1 = LoginText.Text.Trim();
        employee.Password = PasswordText.Text; // ← Хэшируем пароль!
        employee.TabNumEmployeeNavigation.IdDepart = selectedDepart.IdDepart;
        employee.TabNumEmployeeNavigation.Chief = selectedChief.TabNumEmployee;

       
            if (VariableData.selectUser == null)
            {
                App.DbContext.Logins.Add(employee);
            }
            else
            {
                App.DbContext.Update(employee);
            }

            await App.DbContext.SaveChangesAsync(); // ← лучше использовать асинхронно
            this.Close();
        
    }}