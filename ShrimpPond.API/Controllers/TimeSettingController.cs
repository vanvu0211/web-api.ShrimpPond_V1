using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.Food.Commands.CreateNewFood;
using ShrimpPond.Application.Feature.TimeSetting.Command.CreateTimeSetting;
using ShrimpPond.Application.Feature.TimeSetting.Queries.GetTimeSetting;

namespace ShrimpPond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class TimeSettingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TimeSettingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetTimeSetting([FromQuery] int farmId)
        {
            var timeSettings = await _mediator.Send(new GetTimeSetting() { farmId = farmId} );
            return Ok(timeSettings);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTimeSetting([FromBody] CreateTimeSetting e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }
    }
}
