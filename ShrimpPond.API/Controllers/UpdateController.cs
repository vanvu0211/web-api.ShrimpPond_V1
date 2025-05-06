using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.Feeding.Commands.Feeding;
using ShrimpPond.Application.Feature.Feeding.Commands.MedicineFeeding;
using ShrimpPond.Application.Feature.Update.Commands.LossShrimpUpdate;
using ShrimpPond.Application.Feature.Update.Commands.SizeShrimpUpdate;
using ShrimpPond.Application.Feature.Update.Queries.GetFoodFeeding;
using ShrimpPond.Application.Feature.Update.Queries.GetLossUpdate;
using ShrimpPond.Application.Feature.Update.Queries.GetMedicineFeeding;
using ShrimpPond.Application.Feature.Update.Queries.GetSizeUpdate;
using ShrimpPond.Domain.Food;

namespace ShrimpPond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UpdateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UpdateController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("SizeShrimp")]
        public async Task<IActionResult> GetSizeShrimp([FromQuery] string? PondId, int pageSize = 200, int pageNumber = 1)
        {
            var sizeShrimps = await _mediator.Send(new GetSizeUpdate());
            if (PondId != null)
            {
                sizeShrimps = sizeShrimps.Where(x => x.PondId == PondId).ToList();
            }
            sizeShrimps = sizeShrimps.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return Ok(sizeShrimps);
        }
        [HttpGet("LossShrimp")]
        public async Task<IActionResult> GetLossShrimp([FromQuery] string? PondId, int pageSize = 200, int pageNumber = 1)
        {
            var lossShrimps = await _mediator.Send(new GetLossUpdate());
            if (PondId != null)
            {
                lossShrimps = lossShrimps.Where(x => x.PondId == PondId).ToList();
            }
            lossShrimps = lossShrimps.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return Ok(lossShrimps);
        }
        [HttpGet("FoodFeeding")]
        public async Task<IActionResult> GetFoodFeeding([FromQuery] DateTime? date, string pondId, int pageSize = 200, int pageNumber = 1)
        {
            var foodFeedings = await _mediator.Send(new GetFoodFeeding { pondId = pondId });

            if (date.HasValue)
            {
                foodFeedings = foodFeedings.Where(x => x.FeedingDate.Date == date.Value.Date).ToList();
            }

            if (!foodFeedings.Any())
            {
                return NotFound("Không tìm thấy dữ liệu lịch sử cho ăn.");
            }

            var paginatedResult = foodFeedings
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(paginatedResult);
        }
        [HttpGet("MedicineFeeding")]
        public async Task<IActionResult> GetMedicineFeeding([FromQuery] DateTime? date, string pondId, int pageSize = 200, int pageNumber = 1)
        {
            var medicineFeedings = await _mediator.Send(new GetMedicineFeeding { pondId = pondId });
            if (date.HasValue)
            {
                medicineFeedings = medicineFeedings.Where(x => x.feedingDate.Date == date.Value.Date).ToList();
            }

            if (!medicineFeedings.Any())
            {
                return NotFound("Không tìm thấy dữ liệu lịch sử cho ăn.");
            }

            var paginatedResult = medicineFeedings
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(paginatedResult);
        }
        [HttpPost("Food")]
        public async Task<IActionResult> FoodFeeding([FromBody] Application.Feature.Feeding.Commands.Feeding.FoodFeeding e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }
        [HttpPost("Medicine")]
        public async Task<IActionResult> MedicineFeeding([FromBody] MedicineFeeding e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }
        [HttpPost("SizeShrimp")]
        public async Task<IActionResult> SizeShrimpUpdate([FromBody] SizeShrimpUpdate sizeShrimpUpdate)
        {
            var id = await _mediator.Send(sizeShrimpUpdate);
            return Ok(sizeShrimpUpdate);
        }
        [HttpPost("LossShrimp")]
        public async Task<IActionResult> LossShrimpUpdate([FromBody] LossShrimpUpdate lossShrimpUpdate)
        {
            var id = await _mediator.Send(lossShrimpUpdate);
            return Ok(lossShrimpUpdate);
        }
    }
}
