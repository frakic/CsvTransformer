using CsvTransformer.App.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace CsvTransformer.App;

public partial class App : Application
{
    private readonly ServiceProvider serviceProvider;

    public App()
    {
        var services = new ServiceCollection();
        ConfigureApplicationServices(services);
        serviceProvider = services.BuildServiceProvider();
    }

    private static void ConfigureApplicationServices(ServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services.AddSingleton<CsvService>();
        services.AddSingleton<FileService>();
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        var mainWindow = serviceProvider.GetService<MainWindow>();
        mainWindow?.Show();
    }
}