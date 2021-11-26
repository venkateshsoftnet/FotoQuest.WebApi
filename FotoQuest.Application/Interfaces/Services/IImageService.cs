using System;
using System.IO;
using System.Threading.Tasks;

using FotoQuest.Domain.Entities;

namespace FotoQuest.Application.Interfaces.Services
{
    public interface IImageService
    {
        Task<MemoryStream> GetFile(Guid id, string filename, ImageType imageType, int size = 0);
    }
}
