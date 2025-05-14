using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.NurseryPond.Commands.ActiveNurseryPond;
using ShrimpPond.Application.Feature.NurseryPond.Commands.CreatePond;
using ShrimpPond.Application.Feature.Pond.Commands.CreateCleanTime;
using ShrimpPond.Application.Feature.Pond.Commands.DeletePond;
using ShrimpPond.Application.Feature.Pond.Commands.HarvestPond;
using ShrimpPond.Application.Feature.Pond.Queries.GetAllPond;
using ShrimpPond.Application.Feature.Pond.Queries.GetHarvestTime;
using ShrimpPond.Application.Feature.Pond.Queries.GetTimeClean;
using ShrimpPond.Application.Feature.Transfer;
using ShrimpPond.Domain.PondData;

namespace ShrimpPond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PondController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PondController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetPonds([FromQuery] int farmId, string? pondId, string? pondTypeName, string? pondTypeId, EPondStatus? pondStatus, int pageSize = 200, int pageNumber = 1)
        {
            var ponds = await _mediator.Send(new GetAllPond() { farmId = farmId });
            if (pondId != null)
            {
                ponds = ponds.Where(x => x.pondId == pondId).ToList();

            }
            if (pondTypeName != null)
            {
                ponds = ponds.Where(x => x.pondTypeName == pondTypeName).ToList();
            }
            if (pondTypeId != null)
            {
                ponds = ponds.Where(x => x.pondTypeId == pondTypeId).ToList();
            }
            if (pondStatus != null)
            {
                ponds = ponds.Where(x => x.status == pondStatus).ToList();
            }
            ponds = ponds.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            //if (ponds.Count() ==0)
            //{
            //    throw new BadRequestException("Not found Pond");
            //}
            ponds = ponds.OrderBy(x => x.pondName).ToList();

            return Ok(ponds);
        }
        [HttpGet("GetHarvestTime")]
        public async Task<IActionResult> GetHarvestTime([FromQuery] string pondId)
        {
            var harvestTime = await _mediator.Send(new GetHarvestTime { pondId = pondId });
            return Ok(harvestTime);
        }

        

        [HttpGet("GetCleanTime")]
        public async Task<IActionResult> GetCleanTime([FromQuery] int farmId)
        {
            var cleanTime = await _mediator.Send(new GetTimeClean() { farmId = farmId });
            return Ok(cleanTime);
        }

        [HttpPost("CleanSensor")]
        public async Task<IActionResult> CleanSensor([FromBody] CreateCleanTime e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }

        [HttpPost("CreatePond")]
        public async Task<IActionResult> CreatePond([FromBody] CreatePond e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }
        [HttpPost("HarvestPond")]
        public async Task<IActionResult> HarvestPond([FromBody] HarvestPond e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }

        [HttpPut("ActivePond")]
        public async Task<IActionResult> ActivePond([FromBody] ActivePond e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }
        [HttpPut("TransferPond")]
        public async Task<IActionResult> TransferPond([FromBody] Transfer e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePond([FromQuery] string PondId)
        {
            var command = new DeletePond { pondId = PondId };
            var idReturn = await _mediator.Send(command);
            return Ok(command);
        }
    }
}
