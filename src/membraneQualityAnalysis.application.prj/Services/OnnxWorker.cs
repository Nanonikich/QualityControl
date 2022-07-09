using System;
using System.Linq;
using System.IO;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.ML.OnnxRuntime;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.Fonts;

namespace membraneQualityAnalysis.application.Services;

public class OnnxWorker
{
	private Image<Rgb24> _image;
	private Tensor<float> _input;
	private List<Prediction> _predictions;

	/// <summary>Обработка кадра.</summary>
	/// <param name="url">Путь к обрабатываемому кадру.</param>
	/// <param name="onnxPath">Путь к модели onnx.</param>
	/// <returns>Обработанное изображение.</returns>
	public BitmapImage FrameProcessing(string url, string onnxPath)
	{
		// чтение изображения
		_image = Image.Load<Rgb24>(url, out IImageFormat format);

		// изменение размера изображения
		float ratio = 800f / Math.Min(_image.Width, _image.Height);
		_image.Mutate(x => x.Resize((int)(ratio * _image.Width), (int)(ratio * _image.Height)));

		Preprocessing();

		// установка входов и выходов
		var inputs = new List<NamedOnnxValue>
		{
			NamedOnnxValue.CreateFromTensor("image", _input)
		};

		// запуск вывода
		using var session = new InferenceSession(onnxPath);
		using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = session.Run(inputs);

		PostProcessing(results);

		Prediction();

		return ConvertFrame(format);
	}

	/// <summary>Предобработка изображения.</summary>
	private void Preprocessing()
	{
		var paddedHeight = (int)(Math.Ceiling(_image.Height / 32f) * 32f);
		var paddedWidth = (int)(Math.Ceiling(_image.Width / 32f) * 32f);
		_input = new DenseTensor<float>(new[] { 3, paddedHeight, paddedWidth });
		var mean = new[] { 102.9801f, 115.9465f, 122.7717f };
		_image.ProcessPixelRows(accessor =>
		{
			for(int y = paddedHeight - accessor.Height; y < accessor.Height; y++)
			{
				Span<Rgb24> pixelSpan = accessor.GetRowSpan(y);
				for(int x = paddedWidth - accessor.Width; x < accessor.Width; x++)
				{
					_input[0, y, x] = pixelSpan[x].B - mean[0];
					_input[1, y, x] = pixelSpan[x].G - mean[1];
					_input[2, y, x] = pixelSpan[x].R - mean[2];
				}
			}
		});
	}

	/// <summary>Постобработка для получения прогноза.</summary>
	private void PostProcessing(IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results)
	{
		var resultsArray = results.ToArray();
		float[] boxes = resultsArray[0].AsEnumerable<float>().ToArray();
		long[] labels = resultsArray[1].AsEnumerable<long>().ToArray();
		float[] confidences = resultsArray[2].AsEnumerable<float>().ToArray();
		_predictions = new List<Prediction>();
		var minConfidence = 0.7f;
		for(int i = 0; i < boxes.Length - 4; i += 4)
		{
			var index = i / 4;
			if(confidences[index] >= minConfidence)
			{
				_predictions.Add(new Prediction
				{
					Box = new Box(boxes[i], boxes[i + 1], boxes[i + 2], boxes[i + 3]),
					//Label = LabelMap.Labels[labels[index]],
					Confidence = confidences[index]
				});
			}
		}
	}

	/// <summary>Расстановка рамок и уверенности на изображении.</summary>
	private void Prediction()
	{
		Font font = SystemFonts.CreateFont("Arial", 16);
		foreach(var p in _predictions)
		{
			_image.Mutate(x =>
			{
				x.DrawLines(Color.Red, 2f, new PointF[] {

					new PointF(p.Box.Xmin, p.Box.Ymin),
					new PointF(p.Box.Xmax, p.Box.Ymin),

					new PointF(p.Box.Xmax, p.Box.Ymin),
					new PointF(p.Box.Xmax, p.Box.Ymax),

					new PointF(p.Box.Xmax, p.Box.Ymax),
					new PointF(p.Box.Xmin, p.Box.Ymax),

					new PointF(p.Box.Xmin, p.Box.Ymax),
					new PointF(p.Box.Xmin, p.Box.Ymin)
				});
				x.DrawText($"{p.Label}, {p.Confidence:0.00}", font, Color.White, new PointF(p.Box.Xmin, p.Box.Ymin));
			});
		}
	}

	/// <summary>Преобразование изображения в выводимый формат.</summary>
	/// <param name="format"></param>
	/// <returns></returns>
	private BitmapImage ConvertFrame(IImageFormat format)
	{
		using(var ms = new MemoryStream())
		{
			_image.Save(ms, format);
			ms.Seek(0, SeekOrigin.Begin);

			var bitmapImage = new BitmapImage();
			bitmapImage.BeginInit();
			bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
			bitmapImage.StreamSource = ms;
			bitmapImage.EndInit();

			return bitmapImage;
		}
	}
}
