using FotoQuest.WebApi.Domain.Entities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FotoQuest.WebApi.Application.Interfaces.Services
{
    public interface IImageService
    {
        Task<MemoryStream> GetFile(Guid id, string filename, ImageType imageType, int size = 0);
    }
}
