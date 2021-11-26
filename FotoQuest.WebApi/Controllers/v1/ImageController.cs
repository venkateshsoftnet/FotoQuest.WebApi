using System.Collections.Generic;
using System.Threading.Tasks;

using FotoQuest.Application.Features.Images.Commands.SaveImage;
using FotoQuest.Application.Model;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FotoQuest.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ImageController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetImageRequestQuery query)
        {
            var response = await Mediator.Send(query);
            
            return File(response.Data.MemoryStream, response.Data.ContentType, response.Data.FileName);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm]List<IFormFile> files)
        {
            var command = new SaveImageCommand
            {
                Files = files
            };

            return Ok(await Mediator.Send(command));
        }
    }
}
