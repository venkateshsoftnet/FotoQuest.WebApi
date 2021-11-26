using FotoQuest.Application.Interfaces;
using FotoQuest.Application.Interfaces.Services;
using FotoQuest.Infrastructure.Shared.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FotoQuest.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient(typeof(IImageService), typeof(ImageService));
        }
    }
}
