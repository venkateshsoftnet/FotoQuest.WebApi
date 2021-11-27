using System.Collections.Generic;
using System.Threading.Tasks;

using FotoQuest.Application.Features.Images.Commands.SaveImage;
using FotoQuest.Application.Model;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FotoQuest.WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class ImageController : BaseApiController
    {
        private readonly ILogger<ImageController> _logger;

        public ImageController(ILogger<ImageController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetImageRequestQuery query)
        {
            _logger.Log(LogLevel.Information, "Begin: Get Image Service");
            
            var response = await Mediator.Send(query);

            _logger.Log(LogLevel.Information, "End: Get Image Service");

            return File(response.Data.MemoryStream, response.Data.ContentType, response.Data.FileName);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm]List<IFormFile> files)
        {
            _logger.Log(LogLevel.Information, "Begin: Save Image Service");

            var command = new SaveImageCommand
            {
                Files = files
            };

            var response = await Mediator.Send(command);

            _logger.Log(LogLevel.Information, "Begin: Save Image Service");

            return Ok(response);
        }
    }
}
