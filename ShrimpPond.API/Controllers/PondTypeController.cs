using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.PondType.Commands.CreatePondType;
using ShrimpPond.Application.Feature.PondType.Commands.DeletePondType;
using ShrimpPond.Application.Feature.PondType.Queries.GetPondType;

namespace ShrimpPond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class PondTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PondTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetPondTypes([FromQuery]  int? farmId, int pageSize = 200, int pageNumber = 1)
        {
            var pondType = await _mediator.Send(new GetPondType());
            if (farmId != null)
            {
                pondType = pondType.Where(x => x.farmId == farmId).ToList();
            }

            pondType = pondType.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return Ok(pondType);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePondType([FromBody] CreatePondType e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePondType([FromQuery] string pondTypeId)
        {
            var command = new DeletePondType { pondTypeId = pondTypeId };
            var idReturn = await _mediator.Send(command);
            return Ok(command);
        }

    }
}
