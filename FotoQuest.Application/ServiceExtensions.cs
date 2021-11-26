using System.Reflection;

using AutoMapper;

using FluentValidation;

using FotoQuest.Application.Behaviours;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace FotoQuest.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        }
    }
}
