using MediatR;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignService.Delete;
using SWD.NextIntern.Service.Services.CampaignService.Update;
using SWD.NextIntern.Service.Services.InternEvaluationService.Create;
using SWD.NextIntern.Service.Services.InternEvaluationService;
using SWD.NextIntern.Service.Services.InternEvaluationService.GetAllInternEvaluation;
using SWD.NextIntern.Service.Services.InternEvaluationService.GetInternEvaluationById;
using SWD.NextIntern.Service.Services.InternEvaluationService.Delete;
using SWD.NextIntern.Service.Services.InternEvaluationService.Update;
using Microsoft.AspNetCore.Authorization;

namespace SWD.NextIntern.API.Controllers.InternEvaluation
{
    [Route("api/v1/intern-evaluation")]
    [ApiController]
    [Authorize]
    public class InternEvaluationController
    {
        private readonly IMediator _mediator;

        public InternEvaluationController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("create")]
        public async Task<ResponseObject<string>> CreateInternEvaluation([FromBody] CreateInternEvaluationCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("all")]
        public async Task<ResponseObject<List<InternEvaluationDto>>> GetAllInternEvaluation(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAllInternEvaluationQuery(), cancellationToken);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ResponseObject<InternEvaluationDto>> GetInternEvaluationById(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetInternEvaluationByIdQuery(id), cancellationToken);
            return result;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<ResponseObject<string>> DeleteInternEvaluation(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteInternEvaluationCommand(id), cancellationToken);
            return result;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("update")]
        public async Task<ResponseObject<string>> UpdateInternEvaluation([FromBody] UpdateInternEvaluationCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }
    }
}
