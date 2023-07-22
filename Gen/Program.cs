using Avalonia;
using Avalonia.ReactiveUI;
using System;
using Microsoft.Extensions.Configuration;

namespace Gen;

public class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    public static IConfigurationRoot configuration;
    [STAThread]
    public static void Main(string[] args)
    {
        /*
        configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsetting.json")
            .Build();
            */
        
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

    }

      

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
}