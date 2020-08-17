using Newtonsoft.Json;
using System.Collections.Generic;

namespace VideoAPI.app.services.models
{
    public class DocumentListItem
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("fileName")]
        public string FileName { get; set; }
        [JsonProperty("path")]
        public string Path { get; set; }
        [JsonProperty("assetName")]
        public string AssetName { get; set; }
        [JsonProperty("encodedAssetName")]
        public string EncodedAssetName { get; set; }
        // [JsonProperty("streamingUrls")]
        // public List<StreamUrlListItem> StreamingUrls { get; set; }
    }
}