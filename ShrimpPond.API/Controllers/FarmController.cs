using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.Farm.Command.CreateFarm;
using ShrimpPond.Application.Feature.Farm.Command.DeleteFarm;
using ShrimpPond.Application.Feature.Farm.Queries.GetAllFarm;

namespace ShrimpPond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FarmController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FarmController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetFarm([FromQuery] string userName, int pageSize = 200, int pageNumber = 1)
        {
            var farms = await _mediator.Send(new GetAllFarm()
            {
                userName = userName
            });
                
            farms = farms.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return Ok(farms);
        }
        [HttpPost]
        public async Task<IActionResult> CreateFarm([FromBody] CreateFarm e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFarm([FromQuery] int farmId)
        {
            var command = new DeleteFarm { farmId = farmId };
            var IdReturn = await _mediator.Send(command);
            return Ok(command);
        }
    }
}
