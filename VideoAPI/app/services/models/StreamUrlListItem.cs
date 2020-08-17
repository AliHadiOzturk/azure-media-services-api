using Newtonsoft.Json;
using VideoAPI.app.models;

namespace VideoAPI.app.services.models
{
    public class StreamUrlListItem
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("streamingProtocol")]
        public StreamProtocol StreamingProtocol { get; set; }
    }
}