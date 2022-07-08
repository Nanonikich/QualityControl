using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace qualityControl.application.Models;

[DataContract]
public class ApplicationConfiguration
{
	[DataMember]
	[JsonProperty("Url")]
	public string Url { get; set; }
}

[DataContract]
public class NetworksConfiguration
{
//	[DataMember]
//	[JsonProperty("Networks")]
//	public string  { get; set; }
}

