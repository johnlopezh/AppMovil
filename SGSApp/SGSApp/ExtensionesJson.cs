using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SGSApp
{
    [DataContract]
    internal class ExtensionesJson
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("uri")] public string Uri { get; set; }

        [JsonProperty("etag")] public string Etag { get; set; }

        [JsonProperty("type")] public string Type { get; set; }
    }

    [DataContract]
    public class ListItemExtensiones
    {
        [JsonProperty("Extension")] public string Extension { get; set; }

        [JsonProperty("ResponsableId")] public string ResponsableId { get; set; }

        [JsonProperty("Title")] public string Title { get; set; }
    }

    [DataContract]
    public class E
    {
        [JsonProperty("results")] public ListItemExtensiones[] Results { get; set; }
    }

    [DataContract]
    public class ListExtensiones
    {
        [JsonProperty("d")] public E E { get; set; }
    }
}