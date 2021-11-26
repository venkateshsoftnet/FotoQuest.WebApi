using FotoQuest.Application.Interfaces.Services;
using FotoQuest.Domain.Entities;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FotoQuest.Infrastructure.Shared.Services
{
    public class ImageService : IImageService
    {
        public async Task<MemoryStream> GetFile(Guid id, string filename, ImageType imageType, int size = 0)
        {
            var imageSize = GetSize(imageType, size);

            return await GetFileFromFolder(id, filename, imageSize, imageSize);
        }

        private async Task<FileData> GetFileStreamAsync(Guid Id, string filename)
        {
            var path = GetFilePath(Id, filename);

            var memory = new MemoryStream();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return new FileData { MemoryStream = memory, ContentType = GetContentType(path), FileName = filename };
        }

        private async Task<MemoryStream> GetFileFromFolder(Guid id, string filename, int width, int height)
        {
            var filedata = await GetFileStreamAsync(id, filename);
            var responseMemoryStream = new MemoryStream();

            using (var image = new MagickImage(filedata.MemoryStream))
            {
                var size = new MagickGeometry(width, height)
                {
                    // This will resize the image to a fixed size without maintaining the aspect ratio.
                    // Normally an image will be resized to fit inside the specified size.
                    IgnoreAspectRatio = true
                };

                image.Resize(size);

                // Save the result
                image.Write(responseMemoryStream);
            }

            return responseMemoryStream;
        }

        private int GetSize(ImageType imageType, int size)
        {
            var imageSizeMap = new Dictionary<ImageType, int> {

                { ImageType.Thumbnail, 128},

                { ImageType.Small, 512},

                { ImageType.Large, 2048},

                { ImageType.Custom, size}
            };

            return imageSizeMap[imageType];
        }

        private static string GetFilePath(Guid Id, string fileName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "images", Id.ToString() + "_" + fileName);
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"}
            };
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();

            return types[ext];
        }

    }
}
