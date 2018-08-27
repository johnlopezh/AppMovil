using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SGSApp
{
    [DataContract]
    internal class UsuarioShJson
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("uri")] public string Uri { get; set; }

        [JsonProperty("etag")] public string Etag { get; set; }

        [JsonProperty("type")] public string Type { get; set; }
    }

    [DataContract]
    public class ListItemRespExtensiones
    {
        [JsonProperty("Title")] public string Title { get; set; }
    }

    [DataContract]
    public class ER
    {
        [JsonProperty("results")] public ListItemRespExtensiones[] Results { get; set; }

        [JsonProperty("Title")] public string Title { get; set; }
    }

    [DataContract]
    public class ListResponsableExtensiones
    {
        [JsonProperty("d")] public ER E { get; set; }
    }
}