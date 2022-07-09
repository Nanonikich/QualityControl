using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace qualityControl.application.Models;

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
	[JsonProperty("detector_cfg")]
	public string DetectorConfig { get; set; }

	[DataMember]
	[JsonProperty("detector_weights")]
	public string DetectorWeights { get; set; }

	[DataMember]
	[JsonProperty("detector_names")]
	public string DetectorNames { get; set; }
}

[DataContract]
public class Classifier
{
	[DataMember]
	[JsonProperty("classifier_path")]
	public string ClassifierPath { get; set; }
}

