using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace FotoQuest.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(System.IO.Path.Combine(System.AppContext.BaseDirectory, "FotoQuest.WebApi.xml"));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "FotoQuest go WebApi",
                    Description = "This Api will be responsible for image data processing."
                   
                });
            });
        }
        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                // Specify the default API Version as 1.0
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
            });
        }
    }
}
