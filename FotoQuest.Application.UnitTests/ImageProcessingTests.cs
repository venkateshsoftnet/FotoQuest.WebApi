using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using FotoQuest.Application.Exceptions;
using FotoQuest.Application.Features.Images.Commands.SaveImage;
using FotoQuest.Application.Features.Images.Queries.GetImageById;
using FotoQuest.Application.Interfaces.Repositories;
using FotoQuest.Application.Interfaces.Services;
using FotoQuest.Application.Mappings;
using FotoQuest.Application.Model;
using FotoQuest.Domain.Entities;

using Microsoft.AspNetCore.Http;

using Moq;

using NUnit.Framework;

namespace FotoQuest.Application.UnitTests
{
    public class ImageProcessingTests
    {
        private Mock<IImageRepositoryAsync> mockImageRepository;
        private Mock<IImageService> mockImageService;
        private IMapper mapper;

        [SetUp]
        public void Setup()
        {
            this.mockImageRepository = new Mock<IImageRepositoryAsync>();
            this.mockImageService = new Mock<IImageService>();

            var myProfile = new GeneralProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            mapper = new Mapper(configuration);
        }

        [Test]
        public void Constructor_IsValid()
        {
            //act
            var saveImageCommandCommandHandler = new SaveImageCommandCommandHandler(this.mockImageRepository.Object, this.mockImageService.Object, mapper);

            //assert
            Assert.NotNull(saveImageCommandCommandHandler);
        }

        [Test]
        public async Task SaveImageCommandCommandHandler_Handle_Success()
        {
            //act
            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "fotoquestsample.jpeg";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var file = fileMock.Object;

            var saveImageCommandCommandHandler = new SaveImageCommandCommandHandler(this.mockImageRepository.Object, this.mockImageService.Object, mapper);

            this.mockImageRepository.Setup(x => x.AddAsync(new Domain.Entities.Image()));

            this.mockImageService.Setup(x => x.SaveImage(System.Guid.Empty, file));
            var request = new SaveImageCommand
            {
                Files = new List<IFormFile>
                {
                    file
                }
            };
            var response = await saveImageCommandCommandHandler.Handle(request, new CancellationToken());

            //assert
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.NotNull(response.Data.Response);
            Assert.AreEqual(response.Data.Response.Count, 1);
            Assert.IsTrue(response.Data.Response.Any(item => item.IsSuccess));
            Assert.IsTrue(response.Data.Response.Any(item => !string.IsNullOrEmpty(item.Id.ToString())));
        }

        [Test]
        public void SaveImageCommandCommandHandlerHandle_Failure()
        {
            //act
            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "fotoquestsample.jpeg";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var file = fileMock.Object;

            this.mockImageService.Setup(x => x.SaveImage(System.Guid.Empty, file));
            var request = new SaveImageCommand
            {
                Files = new List<IFormFile>
                {
                    file
                }
            };

            this.mockImageRepository.Setup(x => x.AddAsync(It.IsAny<Image>())).Throws<Exception>();
            var saveImageCommandCommandHandler = new SaveImageCommandCommandHandler(this.mockImageRepository.Object, this.mockImageService.Object, mapper);

            // Act & Assert       
            Assert.ThrowsAsync<Exception>(() => saveImageCommandCommandHandler.Handle(request, CancellationToken.None));
        }

        [Test]
        public async Task GetImageByIdQueryHandler_Handle_Success()
        {
            //act
            var getImageCommandCommandHandler = new GetImageByIdQueryHandler(this.mockImageRepository.Object, this.mockImageService.Object);
            var guid = Guid.NewGuid();

            this.mockImageRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                 .Returns(Task.FromResult(new Image()
                 {
                     ContentType = "jpg",
                     Id = guid,
                     FileName = "sample.jpg"
                 }));

            var fileDataResponse = new FileDataResponse()
            {
                FileName = "sample.jpg",
                ContentType = "jpg",
                MemoryStream = new MemoryStream()
            };

            this.mockImageService.Setup(x => x.GetImage(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<ImageType>(), It.IsAny<int>()))
                .Returns(Task.FromResult(fileDataResponse));

            var request = new GetImageRequestQuery
            {
                Id = Guid.Empty,
                ImageType = ImageType.Large
            };

            var response = await getImageCommandCommandHandler.Handle(request, new CancellationToken());

            //assert
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.NotNull(response.Data.MemoryStream);
            Assert.AreEqual(response.Data.FileName, "sample.jpg");
            Assert.AreEqual(response.Data.ContentType, "jpg");
        }

        [Test]
        public void GetImageByIdQueryHandler_Failure()
        {
            //act
            var guid = Guid.NewGuid();

            this.mockImageRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                 .Returns(Task.FromResult(new Image()
                 {
                     ContentType = "jpg",
                     Id = guid,
                     FileName = "sample.jpg"
                 }));

            var request = new GetImageRequestQuery
            {
                Id = Guid.Empty,
                ImageType = ImageType.Large
            };

            this.mockImageService.Setup(x => x.GetImage(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<ImageType>(), It.IsAny<int>()))
                .Throws<Exception>();

            var getImageCommandCommandHandler = new GetImageByIdQueryHandler(this.mockImageRepository.Object, this.mockImageService.Object);

            // Act & Assert       
            Assert.ThrowsAsync<Exception>(() => getImageCommandCommandHandler.Handle(request, CancellationToken.None));
        }

        [Test]
        public void GetImageByIdQueryHandler_APIExceptionFailure()
        {
            //act
            var guid = Guid.NewGuid();

            var request = new GetImageRequestQuery
            {
                Id = Guid.Empty,
                ImageType = ImageType.Large
            };

            this.mockImageService.Setup(x => x.GetImage(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<ImageType>(), It.IsAny<int>()))
                .Throws<Exception>();

            var getImageCommandCommandHandler = new GetImageByIdQueryHandler(this.mockImageRepository.Object, this.mockImageService.Object);

            // Act & Assert       
            Assert.ThrowsAsync<ApiException>(() => getImageCommandCommandHandler.Handle(request, CancellationToken.None));
        }
    }
}