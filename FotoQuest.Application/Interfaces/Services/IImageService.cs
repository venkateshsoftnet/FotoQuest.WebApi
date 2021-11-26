using System;
using System.Threading.Tasks;

using FotoQuest.Domain.Entities;

using Microsoft.AspNetCore.Http;

namespace FotoQuest.Application.Interfaces.Services
{
    public interface IImageService
    {
        Task<FileDataResponse> GetImage(Guid id, string filename, ImageType imageType, int size = 0);

        Task SaveImage(Guid Id, IFormFile file);
    }
}
