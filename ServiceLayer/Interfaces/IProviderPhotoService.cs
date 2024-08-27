using ServiceLayer.Dtos;

namespace ServiceLayer.Interfaces
{
    public interface IProviderPhotoService
    {
        Task Insert(ProviderPhotoDto providerPhoto);
    }
}
