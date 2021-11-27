using System.Threading.Tasks;

using FluentAssertions;

using FotoQuest.Application.Model;
using FotoQuest.WebApi.Controllers;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Moq;

using NUnit.Framework;

namespace FotoQuest.WebApi.UnitTests
{
    public class ImageControllerTests
    {
        private Mock<ILogger<ImageController>> mockLogger;
        private static IServiceScopeFactory _scopeFactory;
        private static IConfigurationRoot _configuration;
        public static readonly string MockAppSettingsFileName = "appsettings.integrationtests.json";

        [SetUp]
        public void Setup()
        {
            var builder = new ConfigurationBuilder()
               .AddEnvironmentVariables()
               .AddJsonFile(MockAppSettingsFileName);

            _configuration = builder.Build();

            mockLogger = new Mock<ILogger<ImageController>>();
            var services = new ServiceCollection();

            var startup = new Startup(_configuration);

            services.AddSingleton(Mock.Of<IHostEnvironment>(w =>
                w.EnvironmentName == "Production" &&
                w.ApplicationName == nameof(FotoQuest.WebApi)));

            startup.ConfigureServices(services);


            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
        }

        [Test]
        public async Task Test1()
        {
            var query = new GetImageRequestQuery();
            var response = await SendAsync(query);
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<ISender>();

            return await mediator.Send(request);
        }

    }
}