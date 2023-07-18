using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PreMatriculasParanoa.Web.Admin.Shared.CodeBase.Models
{
    public class RefitApiError
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("traceId")]
        public string TraceId { get; set; }

        [JsonPropertyName("errors")]
        public Dictionary<string, string[]> Errors { get; set; }
    }
}
