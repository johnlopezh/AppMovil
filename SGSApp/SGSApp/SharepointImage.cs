using Newtonsoft.Json;

namespace SGSApp
{
    public class ListItemImage
    {
        [JsonProperty("id")] public string id { get; set; }

        [JsonProperty("uri")] public string uri { get; set; }

        [JsonProperty("type")] public string type { get; set; }
    }

    public class I
    {
        //[JsonProperty("__metadata")] public ListItemImage Results { get; set; }

        [JsonProperty("id")] public string id { get; set; }
    }

    public class ListItemModelsImage
    {
        [JsonProperty("id")] public string id { get; set; }
        [JsonProperty("serverProcessedContent")] public ServerProcessedContent serverProcessedContent { get; set; }
    }
}