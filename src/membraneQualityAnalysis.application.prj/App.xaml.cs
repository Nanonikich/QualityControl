using System.Windows;
using membraneQualityAnalysis.application.DI;

namespace membraneQualityAnalysis;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        ViewModelLocator.Init();

        base.OnStartup(e);
    }
}
