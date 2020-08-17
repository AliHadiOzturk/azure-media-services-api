using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoAPI.app.models
{
    [Table("document", Schema = "novus")]
    public class Document
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public string AssetName { get; set; }
        public string EncodedAssetName { get; set; }
        public string EncodeJobName { get; set; }
        public string LocatorName { get; set; }
        public List<DocumentStreamingUrl> StreamingUrls { get; set; } = new List<DocumentStreamingUrl>();
    }
}