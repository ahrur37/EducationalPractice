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
            DbContext.Academics.ToList();
            DbContext.Departments.ToList();
            DbContext.Exams.ToList();
            DbContext.Employees.ToList();
            DbContext.Engineers.ToList();
            DbContext.Logins.ToList();
            DbContext.Faculties.ToList();
            DbContext.Specialties.ToList();
            DbContext.Students.ToList();
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