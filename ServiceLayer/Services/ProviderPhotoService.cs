using Azure.Storage.Blobs;
using DomainLayer.Models;
using RepositoryLayer.IRepository;
using ServiceLayer.Dtos;
using ServiceLayer.Interfaces;

namespace ServiceLayer.Services
{
    public class ProviderPhotoService : IProviderPhotoService
    {
        const string connectionString = "";
        const string containerName = "containerblobcapstone";
        private readonly IProviderPhotoRepository _providerPhotoRepository;
        public ProviderPhotoService(IProviderPhotoRepository providerPhotoRepository)
        {
            _providerPhotoRepository = providerPhotoRepository;
        }
        public async Task Insert(ProviderPhotoDto providerPhoto)
        {
            var containerClient = new BlobContainerClient(connectionString, containerName);
            var uniqueName = Guid.NewGuid().ToString();
            var blobClient = containerClient.GetBlobClient(uniqueName);
            var stream = providerPhoto.File.OpenReadStream();
            await blobClient.UploadAsync(stream, true);

            _providerPhotoRepository.Insert(new ProviderPhoto()
            {
                BlobName = uniqueName,
                FileName = providerPhoto.FileName,
                CreatedDate = DateTime.Now,
                UploadedByUserName = providerPhoto.UploadedByUserName,
                UploadedIp = providerPhoto.UploadedIp
            });
        }
    }
}
