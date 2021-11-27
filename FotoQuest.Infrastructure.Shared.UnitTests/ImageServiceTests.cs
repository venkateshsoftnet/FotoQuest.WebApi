using System;

using FotoQuest.Infrastructure.Shared.Services;

using NUnit.Framework;

namespace FotoQuest.Infrastructure.Shared.UnitTests
{
    public class ImageServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Constructor_IsValid()
        {
            //act
            var guid = Guid.NewGuid();
            var imageService = new ImageService();

            imageService.GetImage(guid, string.Empty, Domain.Entities.ImageType.Large, 0);

            //assert
            Assert.NotNull(saveImageCommandCommandHandler);
        }
    }
}