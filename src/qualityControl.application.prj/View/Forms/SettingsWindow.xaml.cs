using System;
using System.Windows;

namespace qualityControl.application.View.Forms;

/// <summary>
/// Interaction logic for SettingsWindow.xaml
/// </summary>
public partial class SettingsWindow : Window
{
	public SettingsWindow()
	{
		InitializeComponent();
		SettingsViewModel vm = new SettingsViewModel(new Models.ProcessConfiguration());
		DataContext = vm;
		if(vm.CloseAction == null) 
			vm.CloseAction = new Action(Close);
	}
}
