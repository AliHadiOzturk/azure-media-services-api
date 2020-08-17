using System.ComponentModel.DataAnnotations.Schema;

namespace VideoAPI.app.models
{
    [Table("document_streamingurl", Schema = "novus")]
    public class DocumentStreamingUrl
    {
        public long DocumentId { get; set; }
        public Document Document { get; set; }
        public long StreamingUrlId { get; set; }
        public StreamingUrl StreamingUrl { get; set; }
    }
}