using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.Farm.Command.CreateFarm;
using ShrimpPond.Application.Feature.Farm.Command.DeleteFarm;
using ShrimpPond.Application.Feature.Farm.Command.InviteMember;
using ShrimpPond.Application.Feature.Farm.Command.RemoveMember;
using ShrimpPond.Application.Feature.Farm.Queries.GetAllFarm;
using ShrimpPond.Application.Feature.Farm.Queries.GetMemeber;

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
        public async Task<IActionResult> GetFarm([FromQuery] string email, int pageSize = 200, int pageNumber = 1)
        {
            var farms = await _mediator.Send(new GetAllFarm()
            {
                Email = email
            });

            farms = farms.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return Ok(farms);
        }

        [HttpGet("GetMember")]
        public async Task<IActionResult> GetMember([FromQuery]  int farmId )
        {
            var members = await _mediator.Send(new GetMemeber()
            {
               FarmId = farmId
            });
            return Ok(members);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFarm([FromBody] CreateFarm e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }

        [HttpPut("InviteMember")]
        public async Task<IActionResult> InviteMember([FromBody] InviteMember e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }

        [HttpPut("RemoveMember")]
        public async Task<IActionResult> RemoveMember([FromBody] RemoveMember e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteFarm([FromQuery] int farmId, string email)
        {
            var command = new DeleteFarm { FarmId = farmId, Email = email };
            var IdReturn = await _mediator.Send(command);
            return Ok(command);
        }
    }
}
