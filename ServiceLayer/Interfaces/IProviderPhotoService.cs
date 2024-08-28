using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dtos;

namespace ServiceLayer.Interfaces
{
    public interface IProviderPhotoService
    {
        Task Insert(ProviderPhotoDto providerPhoto);
        Task<FileContentResult> DownloadPhoto(int id);
        Task<ProviderPhotoResponse?> Get(int id);
        Task<IEnumerable<ProviderPhotoResponse>> GetAll();
    }
}
