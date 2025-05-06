using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.Food.Commands.CreateNewFood;
using ShrimpPond.Application.Feature.Food.Commands.DeleteFood;
using ShrimpPond.Application.Feature.Food.Queries.GetAllFood;
using ShrimpPond.Application.Feature.Medicine.Commands.CreateMedicine;
using ShrimpPond.Application.Feature.Medicine.Commands.DeleteMedicine;
using ShrimpPond.Application.Feature.Medicine.Commands.UpdateMedicine;
using ShrimpPond.Application.Feature.Medicine.Queries.GetAllMedicine;

namespace ShrimpPond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MedicineController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MedicineController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetMedicine([FromQuery] int farmId, int pageSize = 200, int pageNumber = 1)
        {
            var medicines = await _mediator.Send(new GetAllMedicine(farmId));
          
            medicines = medicines.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return Ok(medicines);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewMedicine([FromBody] CreateMedicine createMedicine)
        {
            var id = await _mediator.Send(createMedicine);
            return Ok(createMedicine);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMedicine([FromBody] UpdateMedicine updateMedicine)
        {
            var id = await _mediator.Send(updateMedicine);
            return Ok(updateMedicine);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteFood([FromQuery] int medicineId)
        {
            var command = new DeleteMedicine 
            {
                medicineId = medicineId,
            };
            var IdReturn = await _mediator.Send(command);
            return Ok(command);
        }
    }
}
