using DomainLayer.Models;
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
        public async Task<IActionResult> UploadProviderPhoto(ProviderPhotoDto providerPhoto)
        {
            if (providerPhoto != null)
            {
                await _providerPhotoService.Insert(providerPhoto);
                return Ok("Created Successfully");
            }
            else
            {
                return BadRequest("Somethingwent wrong");
            }
        }

        [HttpGet(nameof(DownloadProviderPhoto))]
        public async Task<IActionResult> DownloadProviderPhoto(int id)
        {
            var photoFile = await _providerPhotoService.DownloadPhoto(id);
            if (photoFile == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(photoFile);
            }
        }

        [HttpGet(nameof(GetProviderPhoto))]
        public async Task<IActionResult> GetProviderPhoto(int id)
        {
            ProviderPhotoResponse? photo = await _providerPhotoService.Get(id);

            if (photo == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(photo);
            }
        }

        [HttpGet(nameof(GetAllPhotos))]
        public async Task<IEnumerable<ProviderPhotoResponse>> GetAllPhotos()
        {
            return await _providerPhotoService.GetAll();
        }
    }
}
