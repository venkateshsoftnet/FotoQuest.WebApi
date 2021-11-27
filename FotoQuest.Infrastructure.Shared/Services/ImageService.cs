using System;
using System.IO;
using System.Threading.Tasks;

using FotoQuest.Application.Interfaces.Services;
using FotoQuest.Domain.Entities;

using ImageMagick;

using Microsoft.AspNetCore.Http;

namespace FotoQuest.Infrastructure.Shared.Services
{
    public class ImageService : IImageService
    {
        public async Task<FileDataResponse> GetImage(Guid id, string filename, ImageType imageType, int customSize = 0)
        {
            var imageSize = GetImageSize(imageType, customSize);

            return await GetImageFromFileSystem(id, filename, imageSize);
        }

        public async Task SaveImage(Guid Id, IFormFile file)
        {
            var path = GetFilePath(Id, file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        private async Task<MemoryStream> GetMemoryStreamAsync(Guid Id, string filename)
        {
            var filePath = GetFilePath(Id, filename);

            var memory = new MemoryStream();

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;

            return memory;
        }

        private async Task<FileDataResponse> GetImageFromFileSystem(Guid id, string filename, int imageSize)
        {
            var imageData = await GetMemoryStreamAsync(id, filename);
            var fileDataResponse = new FileDataResponse
            {
                MemoryStream = new MemoryStream()
            };
            
            using (var image = new MagickImage(imageData))
            {
                var size = new MagickGeometry(imageSize, imageSize)
                {
                    // This will resize the image to a fixed size without maintaining the aspect ratio.
                    // Normally an image will be resized to fit inside the specified size.
                    IgnoreAspectRatio = true
                };

                image.Resize(size);

                // Save the result
                image.Write(fileDataResponse.MemoryStream);
            }

            return fileDataResponse;
        }

        private int GetImageSize(ImageType imageType, int customSize)
        {
            return imageType switch
            {
                ImageType.Thumbnail => 128,
                ImageType.Small => 512,
                ImageType.Large => 2048,
                ImageType.Custom => customSize,
                _ => 0,
            };
        }

        private static string GetFilePath(Guid Id, string fileName)
        {
            var fileExtension = System.IO.Path.GetExtension(fileName);
            //return Path.Combine(Directory.GetCurrentDirectory(), "Images", Id.ToString() + "_" + fileName);

            return Path.Combine(Directory.GetCurrentDirectory(), "Images", Id.ToString() + "" + fileExtension);
        }
    }
}
