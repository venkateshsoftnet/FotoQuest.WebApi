using FotoQuest.WebApi.Application.Features.Products.Commands.CreateProduct;
using FotoQuest.WebApi.Application.Features.Products.Commands.DeleteProductById;
using FotoQuest.WebApi.Application.Features.Products.Commands.UpdateProduct;
using FotoQuest.WebApi.Application.Features.Products.Queries.GetAllProducts;
using FotoQuest.WebApi.Application.Features.Products.Queries.GetProductById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FotoQuest.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProductController : BaseApiController
    {
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductsParameter filter)
        {

            return Ok(await Mediator.Send(new GetAllProductsQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetProductByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        //[HttpPost]
        //public async Task<IActionResult> PostImage(SaveImageCommand command)
        //{
        //    return Ok(await Mediator.Send(command));
        //}

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(Guid id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteProductByIdCommand { Id = id }));
        }

        //[HttpGet]
        //public async Task<IActionResult> GetImage1([FromQuery] GetImageByIdQuery query)
        //{
        //    return Ok(await Mediator.Send(query));
        //}

        //// POST api/<controller>
        //[HttpPost]
        ////[Authorize]
        //public async Task<IActionResult> Post1(SaveImageCommand command)
        //{
        //    return Ok(await Mediator.Send(command));
        //}
    }
}
