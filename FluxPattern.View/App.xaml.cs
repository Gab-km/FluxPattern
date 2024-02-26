using System.Configuration;
using System.Data;
using System.Windows;
using FluxPattern.View.ViewModels;
using FluxPattern.View.Views;

namespace FluxPattern.View;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly MainWindowViewModel dataContext = new();

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        var targetWindow = new MainWindow
        {
            DataContext = dataContext
        };
        targetWindow.Show();
    }
}

