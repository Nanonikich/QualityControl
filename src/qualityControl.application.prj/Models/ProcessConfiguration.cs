using Newtonsoft.Json;
using System.IO;

namespace qualityControl.application.Models;

public class ProcessConfiguration
{
	const string _filepath = @"..\net6.0-windows\settings.json";

	private ApplicationConfiguration _jsonDeserialized;


	public ProcessConfiguration() => Load();


	/// <summary>Выполняет запись конфигурации в файл.</summary>
	public void Save(string? newUrl)
	{
		_jsonDeserialized.Url = newUrl;
		
		File.WriteAllText(_filepath, JsonConvert.SerializeObject(_jsonDeserialized));
	}

	/// <summary>Выполняет чтение конфигурации из файла.</summary>
	public ApplicationConfiguration Load()
	{
		_jsonDeserialized = JsonConvert.DeserializeObject<ApplicationConfiguration>(File.ReadAllText(_filepath));
		return _jsonDeserialized;
	}
}
