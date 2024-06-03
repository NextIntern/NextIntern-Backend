using MediatR;
using Microsoft.AspNetCore.Mvc;
using NextIntern.Application.Intern;

namespace NextIntern.API.Controllers.Intern
{
    [ApiController]

    public class BangLuongController : ControllerBase
    {
        private readonly ISender _mediator;

        public BangLuongController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("intern")]
        public async Task<ActionResult<List<NextIntern.Domain.Entities.Intern>>> GetAllIntern(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAllQuery(), cancellationToken);
            return Ok(result);
        }
    }
}
