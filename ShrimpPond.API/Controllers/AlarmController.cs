using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.Alarm.Command.FormatAlarm;
using ShrimpPond.Application.Feature.Alarm.Queries.GetAllAlarm;
using ShrimpPond.Application.Feature.Environment.Queries.GetEnvironment;
using ShrimpPond.Application.Feature.Farm.Command.DeleteFarm;

namespace ShrimpPond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AlarmController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AlarmController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetEnvironment([FromQuery] int farmId, DateTime startDate, DateTime endDate, int pageSize = 200, int pageNumber = 1)
        {
            var alarms = await _mediator.Send(new GetAllAlarm() { FarmId = farmId });

            // Lọc và lấy tổng số trước khi phân trang
            var filteredAlarms = alarms
                .Where(x => x.AlarmDate >= startDate)
                .Where(x => x.AlarmDate <= endDate)
                .OrderBy(x => x.AlarmDate)
                .ToList();

            var totalCount = filteredAlarms.Count; // Tổng số cảnh báo sau khi lọc

            // Phân trang
            var paginatedAlarms = filteredAlarms
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            // Trả về object chứa cả danh sách và tổng số
            return Ok(new
            {
                Data = paginatedAlarms,
                TotalCount = totalCount
            });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFarm()
        {
            var command = new FormatAlarm ();
            var IdReturn = await _mediator.Send(command);
            return Ok(command);
        }
    }
}
