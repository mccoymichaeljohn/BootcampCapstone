using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class ProviderPhoto
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string BlobName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UploadedByUserName { get; set; }
        public string UploadedIp { get; set; }
    }
}
