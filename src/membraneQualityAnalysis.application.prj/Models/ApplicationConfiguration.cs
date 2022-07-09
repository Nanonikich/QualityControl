using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace membraneQualityAnalysis.application.Models;

[DataContract]
public class ApplicationConfiguration
{
	[DataMember]
	[JsonProperty("Url")]
	public string? Url { get; set; }

	[DataMember]
	[JsonProperty("Networks")]
	public Networks Networks { get; set; }
}

[DataContract]
public class Networks
{
	[DataMember]
	[JsonProperty("detector")]
	public Detector Detector { get; set; }

	[DataMember]
	[JsonProperty("classifier")]
	public Classifier Classifier { get; set; }
}

[DataContract]
public class Detector
{
	[DataMember]
	[JsonProperty("onnx_path")]
	public string OnnxPath { get; set; }
}

[DataContract]
public class Classifier
{
	[DataMember]
	[JsonProperty("classifier_path")]
	public string ClassifierPath { get; set; }
}

