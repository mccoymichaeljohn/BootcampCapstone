using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DomainLayer.Models;
using Moq;
using RepositoryLayer.IRepository;
using ServiceLayer.Dtos;
using ServiceLayer.Services;

namespace ServiceLayerTests.Services
{
    public class ProviderPhotoServiceTests
    {
        Mock<IProviderPhotoRepository> _providerPhotoRepositoryMock = new();
        Mock<BlobServiceClient> _blobServiceClientMock = new();
        Mock<BlobContainerClient> _blobContainerClientMock = new();
        Mock<BlobClient> _blobClientMock = new();

        [Fact]
        public async Task Get_WithValidRequest_ReturnsValidResponse()
        {
            // Arrange
            var photo = new ProviderPhoto();
            var response = new ProviderPhotoResponse();
            _providerPhotoRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(photo);
            var service = new ProviderPhotoService(_providerPhotoRepositoryMock.Object, _blobServiceClientMock.Object);

            // Act
            ProviderPhotoResponse? result = await service.Get(0);

            // Assert
            Assert.Equal(response.FileName, result.FileName);
            Assert.Equal(response.CreatedDate, result.CreatedDate);
            Assert.Equal(response.UploadedByUserName, result.UploadedByUserName);
            Assert.Equal(response.UploadedIp, result.UploadedIp);
            Assert.Equal(response.Id, result.Id);
        }

        [Fact]
        public async Task GetAll_WithValidRequest_ReturnsValidResponse()
        {
            // Arrange
            var photos = new[] { new ProviderPhoto() };
            var response = new[] { new ProviderPhotoResponse() };
            _providerPhotoRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(photos);
            var service = new ProviderPhotoService(_providerPhotoRepositoryMock.Object, _blobServiceClientMock.Object);

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.Single(result);
            Assert.Equal(response.First().FileName, result.First().FileName);
            Assert.Equal(response.First().CreatedDate, result.First().CreatedDate);
            Assert.Equal(response.First().UploadedByUserName, result.First().UploadedByUserName);
            Assert.Equal(response.First().UploadedIp, result.First().UploadedIp);
            Assert.Equal(response.First().Id, result.First().Id);
        }

        [Fact]
        public async Task Download_WithId_ReturnsValidFile()
        {
            // Arrange
            var photo = new ProviderPhoto() { FileName = "test.jpeg"};
            var blobDownloadResult = BlobsModelFactory.BlobDownloadResult(BinaryData.FromString(""));
            Response<BlobDownloadResult> blobResponse = Response.FromValue(blobDownloadResult, Mock.Of<Response>());
            _providerPhotoRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(photo);
            _blobClientMock.Setup(b => b.DownloadContentAsync()).ReturnsAsync(blobResponse);
            _blobContainerClientMock.Setup(b => b.GetBlobClient(It.IsAny<string>())).Returns(_blobClientMock.Object);
            _blobServiceClientMock.Setup(s => s.GetBlobContainerClient(It.IsAny<string>())).Returns(_blobContainerClientMock.Object);
            var service = new ProviderPhotoService(_providerPhotoRepositoryMock.Object, _blobServiceClientMock.Object);

            // Act
            var result = await service.DownloadPhoto(1);

            // Assert
            Assert.NotNull(result);
        }
    }
}
