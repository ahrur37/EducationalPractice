using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using EducationalPractice.Models;
using EducationalPractice.Data;
using MsBox.Avalonia;

namespace EducationalPractice.Control;

public partial class AuthControl : UserControl
{
    private MainWindow _mainWindow;
    public AuthControl()
    {
        InitializeComponent();
        _mainWindow = (MainWindow)VisualRoot;
    }
    
    private async void Button_Entrance(object? sender, RoutedEventArgs e)
    {
        string login = loginText.Text;
        string password = passwordText.Text;
        var logPass = App.DbContext.Logins.FirstOrDefault(log  => log.Login1 == login 
                                                                 && log.Password == password);
        
         if (logPass != null)
         {
             VariableData.authUser = logPass;
             await MessageBoxManager.GetMessageBoxStandard("УРА", "Авторизация прошла успешно").ShowAsync();
             //NavigationService.NavigateTo<MainControls>();
         }
         else
         {
             await MessageBoxManager.GetMessageBoxStandard("ЭХ", "Неправильно введен логин или пароль").ShowAsync();
         }
    }
}
