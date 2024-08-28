using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [Fact]
        public async Task Get_WithValidRequest_ReturnsValidResponse()
        {
            // Arrange
            var photo = new ProviderPhoto();
            var response = new ProviderPhotoResponse();
            _providerPhotoRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(photo);
            var service = new ProviderPhotoService(_providerPhotoRepositoryMock.Object);

            // Act
            ProviderPhotoResponse? result = await service.Get(0);

            // Assert
            Assert.Equal(response.FileName, result.FileName);
            Assert.Equal(response.CreatedDate, result.CreatedDate);
            Assert.Equal(response.UploadedByUserName, result.UploadedByUserName);
            Assert.Equal(response.UploadedIp, result.UploadedIp);
            Assert.Equal(response.Id, result.Id);
        }
    }
}
