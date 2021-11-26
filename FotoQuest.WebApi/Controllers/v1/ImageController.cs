using FotoQuest.WebApi.Application.Features.Images.Commands.SaveImage;
using FotoQuest.WebApi.Application.Features.Images.Queries.GetImageById;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FotoQuest.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ImageController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetImageByIdQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> Post(SaveImageCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
