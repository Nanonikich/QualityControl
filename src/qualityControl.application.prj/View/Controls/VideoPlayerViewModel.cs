using FastYolo;
using FastYolo.Model;
using qualityControl.application.Models;
using qualityControl.application.MVVM;
using qualityControl.application.View.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace qualityControl.application.View.Controls;

public sealed partial class VideoPlayerViewModel : NotificationObjects
{
	private string _detectorCongif;
	private string _detectorWeights;
	private string _detectorNames;
	//private string _classifierConfig;
	private BitmapImage _images;
	private string _newUrl;

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

	public VideoPlayerViewModel(ProcessConfiguration configuration)
	{
		_configuration = configuration;
		var loadedConfig = _configuration.Load().Networks;

		_detectorCongif = loadedConfig.Detector.DetectorConfig;
		_detectorWeights = loadedConfig.Detector.DetectorWeights;
		_detectorNames = loadedConfig.Detector.DetectorNames;
		//_classifierConfig = loadedConfig.Classifier.ClassifierPath;
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
				ImageProcessing();
			}
			else if(videoFormates.Contains(_newUrl.Split('.').Last()))
			{
				VideoProcessing();
			}
		}
	}

	/// <summary>Процесс обработки изображения.</summary>
	private void ImageProcessing()
	{

		Image image = Image.FromFile(Url);

		YoloWrapper yolo = new(_detectorCongif, _detectorWeights, _detectorNames);

		List<YoloItem> items = yolo.Detect(Url).ToList<YoloItem>();

		Graphics graph = Graphics.FromImage(image);

		foreach(YoloItem item in items)
		{
			Point rectPoint = new(item.X, item.Y);

			graph.DrawRectangle(new Pen(System.Drawing.Color.Yellow, 2), new Rectangle(rectPoint, new Size(item.Width, item.Height)));
			graph.DrawString(item.Type, new Font("Consolas", 10, FontStyle.Bold), new SolidBrush(System.Drawing.Color.Yellow), rectPoint);
		}

		using(var ms = new MemoryStream())
		{
			image.Save(ms, ImageFormat.Bmp);
			ms.Seek(0, SeekOrigin.Begin);

			var bitmapImage = new BitmapImage();
			bitmapImage.BeginInit();
			bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
			bitmapImage.StreamSource = ms;
			bitmapImage.EndInit();

			Images = bitmapImage;
		}
	}

	/// <summary>Процесс обработки видео .</summary>
	private void VideoProcessing()
	{
		//FFMpegCore.VideoStream stream = new();
		//stream.P
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
