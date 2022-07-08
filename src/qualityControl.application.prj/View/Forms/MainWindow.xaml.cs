using qualityControl.application.DI;
using System.Windows;

namespace qualityControl.View.Forms;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	public MainWindow()
	{
		ViewModelLocator.Init();
		InitializeComponent();
	}
}
