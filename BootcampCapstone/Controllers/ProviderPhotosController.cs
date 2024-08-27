using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dtos;
using ServiceLayer.Interfaces;

namespace BootcampCapstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderPhotosController : ControllerBase
    {
        private readonly IProviderPhotoService _providerPhotoService;
        public ProviderPhotosController(IProviderPhotoService providerPhotoService)
        {
            _providerPhotoService = providerPhotoService;
        }

        [HttpPost(nameof(UploadProviderPhoto))]
        public IActionResult UploadProviderPhoto(ProviderPhotoDto providerPhoto)
        {
            if (providerPhoto != null)
            {
                _providerPhotoService.Insert(providerPhoto);
                return Ok("Created Successfully");
            }
            else
            {
                return BadRequest("Somethingwent wrong");
            }
        }
    }
}
