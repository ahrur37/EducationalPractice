using System.Linq;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using EducationalPractice.Data;
using Application = Avalonia.Application;

namespace EducationalPractice
{
    public partial class App : Application
    {
        public static AppDbContext DbContext { get;private set; } = new AppDbContext();
        
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            DbContext.Faculties.ToList(); // Название и абривиатура факультета
            DbContext.Departments.ToList(); // Название, шифр Кафедры и Id_факультета
            DbContext.Employees.ToList(); // Информация о сотрудниках
            DbContext.Logins.ToList(); // ДОБАВЛЕНАЯ ТАБЛИЦА логин и пароль сотрудников
            DbContext.Specialties.ToList(); // Шифр кафедры, направление, номер направления
            DbContext.Disciplines.ToList(); // Код специальности, трудоёмкость, название дисциплины,
            DbContext.Applications.ToList(); // Связывает специальности и дисциплины
            DbContext.HeadOfDepts.ToList(); // Дополнительная информация о завкафах
            DbContext.Engineers.ToList(); // Дополнительная информация об инженерах
            DbContext.Teachers.ToList(); // Дополнительная информация о преподавателях
            DbContext.Students.ToList(); // Список студентов
            DbContext.Exams.ToList(); // Журнал экзаменов
            DbContext.Classrooms.ToList(); //
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}