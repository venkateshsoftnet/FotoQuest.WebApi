using FotoQuest.Application.Interfaces.Repositories;
using FotoQuest.Domain.Entities;
using FotoQuest.Infrastructure.Persistence.Contexts;
using FotoQuest.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace FotoQuest.Infrastructure.Persistence.Repositories
{
    public class ImageRepositoryAsync : GenericRepositoryAsync<Image>, IImageRepositoryAsync
    {
        private readonly DbSet<Image> _images;

        public ImageRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _images = dbContext.Set<Image>();
        }        
    }
}
