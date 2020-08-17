using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Azure.Management.Media.Models;

namespace VideoAPI.app.models
{
    [Table("streamingurl", Schema = "novus")]
    public class StreamingUrl
    {
        public long Id { get; set; }
        public string Url { get; set; }
        // List<string> Urls { get; set; }
        public StreamProtocol StreamingProtocol { get; set; }
        public List<DocumentStreamingUrl> Documents { get; set; } = new List<DocumentStreamingUrl>();
    }
}