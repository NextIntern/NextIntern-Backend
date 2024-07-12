using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.InternService.Create;
using SWD.NextIntern.Service.Services.InternService.Delete;
using SWD.NextIntern.Service.Services.InternService.GetAll;
using SWD.NextIntern.Service.Services.InternService.GetById;
using SWD.NextIntern.Service.Services.InternService.Update;
using System.Threading;

namespace SWD.NextIntern.API.Controllers.Intern
{
    [ApiController]
    [Route("api/v1/intern")]
    [Authorize(Policy = "AdminPolicy")]
    public class InternController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InternController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("all")]
        public async Task<ResponseObject<List<InternDto>>> GetAllIntern(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAllQuery(), cancellationToken);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ResponseObject<InternDto?>> GetInternById(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetInternByIdQuery(id), cancellationToken);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ResponseObject<string>> DeleteIntern(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteInternCommand(id), cancellationToken);
            return result;
        }

        [HttpPut("update")]
        public async Task<ResponseObject<string>> UpdateIntern([FromBody] UpdateInternCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateIntern([FromBody] CreateInternCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                var token = await _mediator.Send(command, default);
                if (token == null)
                    return Unauthorized();
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
