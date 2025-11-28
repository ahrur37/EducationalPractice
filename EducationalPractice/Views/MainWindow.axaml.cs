using Avalonia.Controls;
using EducationalPractice.Control;

namespace EducationalPractice
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigationService.Initialize(MainControl);
            NavigationService.NavigateTo<AuthControl>();
        }
    }
}