using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.Configuration.Command.SetConfig;
using ShrimpPond.Application.Feature.Configuration.Queries.GetConfig;
using ShrimpPond.Application.Feature.Environment.Queries.CreateEnvironment;

namespace ShrimpPond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConfigurationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SetConfig([FromBody] SetConfig e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }

        [HttpGet]
        public async Task<IActionResult> GetConfig([FromQuery] int farmId )
        {
            var configs = await _mediator.Send(new GetConfig() { FarmId = farmId});
            return Ok(configs);
        }
    }
}
