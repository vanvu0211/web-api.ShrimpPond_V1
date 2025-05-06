using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.Environment.Queries.CreateEnvironment;
using ShrimpPond.Application.Feature.Environment.Queries.GetEnvironment;
using ShrimpPond.Application.Feature.Farm.Command.CreateFarm;
using ShrimpPond.Application.Feature.Food.Queries.GetAllFood;
using ShrimpPond.Application.Feature.Pond.Queries.GetAllPond;

namespace ShrimpPond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class EnvironmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EnvironmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetEnvironment([FromQuery] string? pondId,string? name, DateTime startDate, DateTime endDate, int pageSize = 200, int pageNumber = 1)
        {
            var environments = await _mediator.Send(new GetEnvironment());

            if (pondId != null)
            {
                environments = environments.Where(x => x.PondId == pondId).ToList();
            }

            if (name != null)
            {
                environments = environments.Where(x => x.Name == name).ToList();
            }

            environments = environments
                .Where(x => x.Timestamp >= startDate)
                .Where(x => x.Timestamp <= endDate)
                .OrderByDescending(x => x.Timestamp)
                .Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return Ok(environments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFarm([FromBody] CreateEnvironment e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }
    }
}
