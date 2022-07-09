using System.Linq;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using membraneQualityAnalysis.application.Models;
using membraneQualityAnalysis.application.MVVM;
using membraneQualityAnalysis.application.Services;
using membraneQualityAnalysis.application.View.Forms;

namespace membraneQualityAnalysis.application.View.Controls;

public sealed partial class VideoPlayerViewModel : NotificationObjects
{
	private string _onnxPath;
	private string _newUrl;
	private BitmapImage _images;
	private OnnxWorker _onnxWorker;

	public string Url 
	{
		get => _newUrl;
		set => UpdatePropertyValue(ref _newUrl, value);
	}

	public BitmapImage Images
	{
		get => _images;
		set => UpdatePropertyValue(ref _images, value);
	}

	public ProcessConfiguration _configuration;

	public VideoPlayerViewModel(ProcessConfiguration configuration, OnnxWorker onnxWorker)
	{
		_configuration = configuration;
		_onnxWorker = onnxWorker;
		_onnxPath = _configuration.Load().Networks.Detector.OnnxPath;
	}

	/// <summary>Проверка воспроизводимого файла.</summary>
	private void CheckUrlString()
	{
		var imageFormates = new List<string>() { "png", "jpg", "bmp" };
		var videoFormates = new List<string>() { "mp4", "avi", "mov", "mkv", "swf", "flv" };

		if(_newUrl != default)
		{
			if(imageFormates.Contains(_newUrl.Split('.').Last()))
			{
				Images = _onnxWorker.FrameProcessing(Url, _onnxPath);
			}
			else if(videoFormates.Contains(_newUrl.Split('.').Last()))
			{
				VideoProcessing();
			}
		}
	}

	/// <summary>Процесс обработки видео .</summary>
	private void VideoProcessing()
	{
		//FFMpegCore.VideoStream stream = new();
		// будет добавлен в дальнейших версиях программы
	}

	/// <summary>Запуск видеоплеера.</summary>
	public void StartPlayer()
	{
		Url = _configuration.Load().Url;
		CheckUrlString();
	}

	/// <summary>Остановка видеоплеера.</summary>
	public void StopPlayer() => Images = default;

	/// <summary>Открытие окна настроек.</summary>
	public void SettingsPlayer()
		=> new SettingsWindow().ShowDialog();
}
