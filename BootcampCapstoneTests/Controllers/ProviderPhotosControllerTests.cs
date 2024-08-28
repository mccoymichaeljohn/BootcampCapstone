using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BootcampCapstone.Controllers;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServiceLayer.Dtos;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;

namespace BootcampCapstoneTests.Controllers
{
    public class ProviderPhotosControllerTests
    {
        private readonly Mock<IProviderPhotoService> _mockService = new();

        [Fact]
        public async Task GetPhoto_WithValidId_ShouldReturnResponse()
        {
            // Arrange
            var providerPhoto = new ProviderPhotoResponse();
            _mockService.Setup(s => s.Get(It.IsAny<int>())).ReturnsAsync(providerPhoto);
            var controller = new ProviderPhotosController(_mockService.Object);

            // Act
            var result = await controller.GetProviderPhoto(1);

            // Assert
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(result);
            ProviderPhotoResponse model = Assert.IsType<ProviderPhotoResponse>(okObjectResult.Value);
            Assert.Equal(providerPhoto, model);
            _mockService.Verify(s => s.Get(1), Times.Once());
        }

        [Fact]
        public async Task GetPhoto_WithInvalidId_ShouldReturnResponse()
        {
            // Arrange
            _mockService.Setup(s => s.Get(It.IsAny<int>())).ReturnsAsync((ProviderPhotoResponse?)null);
            var controller = new ProviderPhotosController(_mockService.Object);

            // Act
            var result = await controller.GetProviderPhoto(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            _mockService.Verify(s => s.Get(1), Times.Once());
        }

        [Fact]
        public async Task GetAll_WithResponse_ReturnsResponse()
        {
            // Arrange
            var providerPhotos = new[] { new ProviderPhotoResponse() };
            _mockService.Setup(s => s.GetAll()).ReturnsAsync(providerPhotos);
            var controller = new ProviderPhotosController(_mockService.Object);

            // Act
            var result = await controller.GetAllPhotos();

            // Assert
            IEnumerable<ProviderPhotoResponse> model = Assert.IsType<ProviderPhotoResponse[]>(result);
            Assert.Equal(providerPhotos, model);
            _mockService.Verify(s => s.GetAll(), Times.Once());
        }

        [Fact]
        public async Task GetAll_WithEmptyResponse_ReturnsEmptyResponse()
        {
            // Arrange
            var providerPhotos = new ProviderPhotoResponse[] { };
            _mockService.Setup(s => s.GetAll()).ReturnsAsync(providerPhotos);
            var controller = new ProviderPhotosController(_mockService.Object);

            // Act
            var result = await controller.GetAllPhotos();

            // Assert
            IEnumerable<ProviderPhotoResponse> model = Assert.IsType<ProviderPhotoResponse[]>(result);
            Assert.Equal(providerPhotos, model);
            _mockService.Verify(s => s.GetAll(), Times.Once());
        }

        [Fact]
        public async Task Download_WithValidResponse_ReturnsOkObjectResult()
        {
            // Arrange
            var file = new FileContentResult(new byte[] { }, "application/json");
            _mockService.Setup(s => s.DownloadPhoto(It.IsAny<int>())).ReturnsAsync(file);
            var controller = new ProviderPhotosController(_mockService.Object);

            // Act
            var result = await controller.DownloadProviderPhoto(1);

            // Assert
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(result);
            FileContentResult model = Assert.IsType<FileContentResult>(okObjectResult.Value);
            Assert.Equal(file, model);
            _mockService.Verify(s => s.DownloadPhoto(1), Times.Once());
        }

        [Fact]
        public async Task Upload_WithValidResponse_ReturnsOk()
        {
            // Arrange
            var providerPhotoDto = new ProviderPhotoDto();
            var controller = new ProviderPhotosController(_mockService.Object);

            // Act
            var result = await controller.UploadProviderPhoto(providerPhotoDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            _mockService.Verify(s => s.Insert(providerPhotoDto), Times.Once());
        }
    }
}
