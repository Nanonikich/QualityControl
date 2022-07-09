using Microsoft.Win32;
using qualityControl.application.Models;
using qualityControl.application.MVVM;
using qualityControl.application.View.Controls;
using System;
using System.ComponentModel;
using System.IO;

namespace qualityControl.application.View.Forms;

public sealed partial class SettingsViewModel : NotificationObjects
{
	private bool _applied;
	private string? _oldPath;
	private string? _selectedPath;

	public Action CloseAction { get; set; }
	public ProcessConfiguration Configuration { get; set; }
	public string? SelectedPath
	{
		get => _selectedPath;
		set => UpdatePropertyValue(ref _selectedPath, value);
	}


	public SettingsViewModel(ProcessConfiguration configuration)
	{
		Configuration = configuration;
		SelectedPath = configuration.Load().Url;
	}

	/// <summary>Визуальное изменение пути к файлу.</summary>
	public void FileSource()
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();

		if(openFileDialog.ShowDialog() == true)
		{
			_oldPath = SelectedPath;
			if(openFileDialog.FileName != default)
			{
				SelectedPath = Path.GetFullPath(openFileDialog.FileName);
			}
		}
	}

	/// <summary>Применение изменённого пути.</summary>
	public void Apply()
	{
		Configuration.Save(SelectedPath);
		CloseAction();
	}

	public void Cancel() => CloseAction();

	public void OnWindowClosing(object sender, CancelEventArgs e)
	{
		if(!_applied) SelectedPath = _oldPath;
	}
}

