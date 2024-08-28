using System.Collections.Generic;
using System.IO;
using Azure.Storage.Blobs;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
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
           // var uniqueName = Guid.NewGuid().ToString();
            var blobClient = containerClient.GetBlobClient(providerPhoto.File.FileName);
            var stream = providerPhoto.File.OpenReadStream();
            await blobClient.UploadAsync(stream);

            await _providerPhotoRepository.Insert(new ProviderPhoto()
            {
                //BlobName = uniqueName,
                FileName = providerPhoto.File.FileName,
                CreatedDate = DateTime.Now,
                UploadedByUserName = providerPhoto.UploadedByUserName,
                UploadedIp = providerPhoto.UploadedIp
            });
        }

        public async Task<FileContentResult> DownloadPhoto(int id)
        {
            var photo = await _providerPhotoRepository.Get(id);

            var containerClient = new BlobContainerClient(connectionString, containerName);
            var blobClient = containerClient.GetBlobClient(photo.FileName);
            var blobResult = await blobClient.DownloadContentAsync();
            new FileExtensionContentTypeProvider().TryGetContentType(photo.FileName, out string? contentType);

            return new FileContentResult(blobResult.Value.Content.ToArray(), contentType);
        }
        public async Task<ProviderPhotoResponse?> Get(int id)
        {
            var photo = await _providerPhotoRepository.Get(id);

            return new ProviderPhotoResponse() { 
                Id = photo.Id,
                CreatedDate = photo.CreatedDate,
                //File = file,
                UploadedByUserName = photo.UploadedByUserName,
                UploadedIp = photo.UploadedIp,
                FileName = photo.FileName
            };
        }

        public async Task<IEnumerable<ProviderPhotoResponse>> GetAll()
        {
            return _providerPhotoRepository.GetAll().Select(photo => new ProviderPhotoResponse()
            {
                Id = photo.Id,
                CreatedDate = photo.CreatedDate,
                //File = file,
                UploadedByUserName = photo.UploadedByUserName,
                UploadedIp = photo.UploadedIp,
                FileName = photo.FileName
            }).ToList();
        }
    }
}
