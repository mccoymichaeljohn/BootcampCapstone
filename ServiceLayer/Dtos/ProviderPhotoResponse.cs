using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ServiceLayer.Dtos
{
    public class ProviderPhotoResponse
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UploadedByUserName { get; set; }
        public string UploadedIp { get; set; }
        public string FileName { get; set; }
        public FileContentResult File { get; set; }
    }
}
