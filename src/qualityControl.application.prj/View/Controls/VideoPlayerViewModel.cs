using qualityControl.application.Models;
using qualityControl.application.MVVM;
using qualityControl.application.View.Forms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace qualityControl.application.View.Controls;

public sealed partial class VideoPlayerViewModel : NotificationObjects
{
	private string _newUrl;
	public string Url 
	{
		get => _newUrl;
		set => UpdatePropertyValue(ref _newUrl, value);
	}

	public ProcessConfiguration _configuration;

	public VideoPlayerViewModel(ProcessConfiguration configuration)
	{
		_configuration = configuration;
	}

	/// <summary>Проверка воспроизводимого файла.</summary>
	private void CheckUrlString()
	{
		var formates = new List<string>() { "mp4", "avi", "png", "jpg", "bmp" };

		if(_newUrl == default || !formates.Contains(_newUrl.Split('.').Last()))
			throw new Exception("Неверный формат файла");
	}

	/// <summary>Запуск видеоплеера.</summary>
	public void StartPlayer()
	{
		Url = _configuration.Load().Url;
		CheckUrlString();
	}

	/// <summary>Остановка видеоплеера.</summary>
	public void StopPlayer() => Url = default;

	/// <summary>Открытие окна настроек.</summary>
	public void SettingsPlayer()
		=> new SettingsWindow().ShowDialog();
}
