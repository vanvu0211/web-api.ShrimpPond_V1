using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.Food.Queries.GetAllFood;
using ShrimpPond.Application.Feature.InformationPond.Queries.GetCertificateFile;
using ShrimpPond.Application.Feature.InformationPond.Queries.GetInformationPond;

namespace ShrimpPond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InfoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InfoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetInfo([FromQuery] string pondId, int pageSize = 200, int pageNumber = 1)
        {
            var infos = await _mediator.Send(new GetInformationPond() { pondId = pondId });

            
            return Ok(infos);
        }
        [HttpGet("FileData")]
        public async Task<IActionResult> GetFileData([FromQuery] int certificateId)
        {
            var infos = await _mediator.Send(new GetCertificateFile() { certificateId = certificateId });
            return Ok(infos);
        }
    }
}
