using System;
using Newtonsoft.Json;

namespace SGSApp
{
    internal class EstudianteJson
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("uri")] public string Uri { get; set; }

        [JsonProperty("etag")] public string Etag { get; set; }

        [JsonProperty("type")] public string Type { get; set; }
    }


    public class ListItemEstudiante
    {
        [JsonProperty("FileSystemObjectType")] public int FileSystemObjectType { get; set; }

        [JsonProperty("Id")] public int Id { get; set; }

        [JsonProperty("ID")] public int ID { get; set; }

        [JsonProperty("ContentTypeId")] public string ContentTypeId { get; set; }

        [JsonProperty("Title")] public string Title { get; set; }


        [JsonProperty("OData__Comments")] public string OData__Comments { get; set; }

        [JsonProperty("PublishingPageContent")]
        public string PublishingPageContent { get; set; }

        [JsonProperty("Summary")] public string Summary { get; set; }

        [JsonProperty("Modified")] public string Modified { get; set; }

        [JsonProperty("Created")] public string Created { get; set; }

        [JsonProperty("AuthorId")] public int AuthorId { get; set; }

        [JsonProperty("EditorId")] public int EditorId { get; set; }

        [JsonProperty("OData__UIVersionString")]
        public string ODataUIVersionString { get; set; }

        [JsonProperty("Attachments")] public bool Attachments { get; set; }

        [JsonProperty("GUID")] public string GUID { get; set; }

        [JsonProperty("Priority")] public string Priority { get; set; }

        [JsonProperty("Status")] public string Status { get; set; }

        [JsonProperty("PercentComplete")] public object PercentComplete { get; set; }

        [JsonProperty("AssignedToId")] public object AssignedToId { get; set; }

        [JsonProperty("TaskGroupId")] public object TaskGroupId { get; set; }

        [JsonProperty("Body")] public object Body { get; set; }

        [JsonProperty("StartDate")] public string StartDate { get; set; }

        [JsonProperty("EventDate")] public DateTime EventDate { get; set; }

        [JsonProperty("fAllDayEvent")] public bool fAllDayEvent { get; set; }

        [JsonProperty("Description")] public string Description { get; set; }

        [JsonProperty("DueDate")] public object DueDate { get; set; }

        [JsonProperty("RelatedItems")] public object RelatedItems { get; set; }
    }

    public class EstudianteItems
    {
        [JsonProperty("DataList")] public ListItemEstudiante[] Results { get; set; }
    }

    public class ListEstudiante
    {
        [JsonProperty("Nombre")] public ListItemEstudiante[] Results { get; set; }
    }
}