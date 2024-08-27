using Microsoft.AspNetCore.Http;

namespace ServiceLayer.Dtos
{
    public class ProviderPhotoDto
    {
        public string FileName { get; set; }
        public string UploadedByUserName { get; set; }
        public string UploadedIp { get; set; }
        public IFormFile File { get; set; }
    }
}
