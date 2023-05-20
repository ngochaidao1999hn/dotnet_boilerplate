using Application.Commands.ProductsCommands;
using Application.Dtos.Product;
using Application.Queries.ProductQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : BaseController
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try 
            {
                var res = await _mediator.Send(new GetProductsQuery());
                return Ok(res);
            } 
            catch (Exception e)
            { 
                return BadRequest(e.Message);
            }                      
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddProductDto model)
        {
            var res = await _mediator.Send(new AddProductRequest(model));
            if (res is not null)
            {
                return Ok(res);
            }
            return BadRequest();
        }
    }
}