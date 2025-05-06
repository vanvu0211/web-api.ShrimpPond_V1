using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.Pond.Queries.GetAllPond;
using ShrimpPond.Application.Feature.Traceability.Queries.GetSeedId;
using ShrimpPond.Application.Feature.Traceability.Queries.GetTimeHarvest;
using ShrimpPond.Application.Feature.Traceability.Queries.GetTraceability;
using ShrimpPond.Domain.Farm;

namespace ShrimpPond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class TraceabilityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TraceabilityController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetTraceability([FromQuery] int farmId, string seedId, int harvestTime, int pageSize = 200, int pageNumber = 1)
        {
            var et = await _mediator.Send(new GetTraceability
            {
                seedId = seedId,
                harvestTime = harvestTime,
                farmId = farmId

            });
            
            return Ok(et);

        }
        [HttpGet("GetSeedId")]
        public async Task<IActionResult> GetSeedId([FromQuery] int farmId, int pageSize = 200, int pageNumber = 1)
        {
            var et = await _mediator.Send(new GetSeedId() { farmId = farmId});

            return Ok(et);

        }

        [HttpGet("GetTimeHarvest")]
        public async Task<IActionResult> GetTimeHarvest([FromQuery] int farmId, int pageSize = 200, int pageNumber = 1)
        {
            var et = await _mediator.Send(new GetTimeHarvest() { farmId = farmId });

            return Ok(et);

        }
    }
}
