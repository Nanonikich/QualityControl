using Microsoft.Extensions.DependencyInjection;
using membraneQualityAnalysis.application.Models;
using membraneQualityAnalysis.application.Services;
using membraneQualityAnalysis.application.View.Controls;
using membraneQualityAnalysis.application.View.Forms;

namespace membraneQualityAnalysis.application.DI;

public class ViewModelLocator
{
    private static ServiceProvider _provider;

    public static void Init()
    {
        var services = new ServiceCollection();

        services
            .AddSingleton<ProcessConfiguration>()
            .AddSingleton<DetectionViewModel>()
            .AddSingleton<VideoPlayerViewModel>()
            .AddSingleton<SettingsViewModel>()
            .AddSingleton<OnnxWorker>();

        _provider = services.BuildServiceProvider();

        foreach(var item in services)
        {
            _provider.GetRequiredService(item.ServiceType);
        }
    }

    public DetectionViewModel DetectionViewModel => _provider.GetRequiredService<DetectionViewModel>();
    public VideoPlayerViewModel VideoPlayerViewModel => _provider.GetRequiredService<VideoPlayerViewModel>();
    public SettingsViewModel SettingsViewModel => _provider.GetRequiredService<SettingsViewModel>();
}
