using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.Farm.Command.CreateFarm;
using ShrimpPond.Application.Feature.Farm.Command.DeleteFarm;
using ShrimpPond.Application.Feature.Farm.Queries.GetAllFarm;
using ShrimpPond.Application.Feature.Machine.Command.CreateMachine;
using ShrimpPond.Application.Feature.Machine.Command.DeleteMachine;
using ShrimpPond.Application.Feature.Machine.Command.UpdateMachine;
using ShrimpPond.Application.Feature.Machine.Queries.GetALlMachine;
using ShrimpPond.Application.Feature.Machine.Queries.GetMachineByPondId;
using ShrimpPond.Domain.Farm;

namespace ShrimpPond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MachineController : ControllerBase
    {

        private readonly IMediator _mediator;

        public MachineController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetMachine([FromQuery] int farmId,  int pageSize = 200, int pageNumber = 1)
        {
            var machines = await _mediator.Send(new GetALlMachine() { farmId = farmId });
            machines = machines.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return Ok(machines);
        }
        [HttpGet("ByPondId")]
        public async Task<IActionResult> GetMachineById([FromQuery] string pondId, int pageSize = 200, int pageNumber = 1)
        {
            var machines = await _mediator.Send(new GetMachineByPondId() { pondId = pondId });
            return Ok(machines);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMachine([FromBody] CreateMachine e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMachine([FromBody] UpdateMachine e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMachine([FromQuery] int machineId)
        {
            var command = new DeleteMachine { machineId = machineId };
            var IdReturn = await _mediator.Send(command);
            return Ok(command);
        }
    }
}
