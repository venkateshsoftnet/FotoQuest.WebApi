using FotoQuest.WebApi.Application.Interfaces.Repositories;
using FotoQuest.WebApi.Domain.Entities;
using FotoQuest.WebApi.Infrastructure.Persistence.Contexts;
using FotoQuest.WebApi.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace FotoQuest.WebApi.Infrastructure.Persistence.Repositories
{
    public class ImageRepositoryAsync : GenericRepositoryAsync<Image>, IImageRepositoryAsync
    {
        private readonly DbSet<Image> _images;

        public ImageRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _images = dbContext.Set<Image>();
        }

        //public Task<bool> IsUniqueBarcodeAsync(string barcode)
        //{
        //    return _images
        //        .AllAsync(p => p.Barcode != barcode);
        //}
    }
}
