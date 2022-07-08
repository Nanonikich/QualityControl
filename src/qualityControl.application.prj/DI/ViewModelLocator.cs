using Microsoft.Extensions.DependencyInjection;
using qualityControl.application.Models;
using qualityControl.application.View.Controls;
using qualityControl.application.View.Forms;

namespace qualityControl.application.DI;

public class ViewModelLocator
{
    private static ServiceProvider _provider;

    public static void Init()
    {
        var services = new ServiceCollection();

        services
            .AddSingleton<ProcessConfiguration>()
            .AddSingleton<ClassifierViewModel>()
            .AddSingleton<VideoPlayerViewModel>()
            .AddSingleton<SettingsViewModel>();

        _provider = services.BuildServiceProvider();

        foreach(var item in services)
        {
            _provider.GetRequiredService(item.ServiceType);
        }
    }

    public ClassifierViewModel ClassifierViewModel => _provider.GetRequiredService<ClassifierViewModel>();
    public VideoPlayerViewModel VideoPlayerViewModel => _provider.GetRequiredService<VideoPlayerViewModel>();
    public SettingsViewModel SettingsViewModel => _provider.GetRequiredService<SettingsViewModel>();
}
