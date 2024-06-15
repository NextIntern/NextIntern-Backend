using MediatR;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.InvernEvaluationService.Create;

namespace SWD.NextIntern.API.Controllers.InternEvaluation
{
    [Route("api/v1/intern-evaluation")]
    [ApiController]
    public class InternEvaluationController
    {
        private readonly IMediator _mediator;

        public InternEvaluationController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("create")]
        public async Task<ResponseObject<string>> CreateInternEvaluation([FromBody] CreateInternEvaluationCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }
    }
}
