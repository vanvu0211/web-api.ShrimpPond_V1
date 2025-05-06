using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.Food.Commands.CreateNewFood;
using ShrimpPond.Application.Feature.Food.Commands.DeleteFood;
using ShrimpPond.Application.Feature.Food.Commands.UpdateFood;
using ShrimpPond.Application.Feature.Food.Queries.GetAllFood;
using ShrimpPond.Application.Feature.Pond.Commands.DeletePond;
using ShrimpPond.Application.Feature.PondType.Queries.GetPondType;

namespace ShrimpPond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FoodController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FoodController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetFood([FromQuery]  int farmId, int pageSize = 200, int pageNumber = 1)
        {
            var foods = await _mediator.Send(new GetAllFood(farmId));
           
            foods = foods.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return Ok(foods);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewFood([FromBody] CreateNewFood e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateFood([FromBody] UpdateFood e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFood([FromQuery] int foodId)
        {
            var command = new DeleteFood 
            {
                foodId = foodId,
            };
            var IdReturn = await _mediator.Send(command);
            return Ok(command);
        }
    }
}
