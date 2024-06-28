using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.InternEvaluationCriteriaService.Create;
using SWD.NextIntern.Service.Services.InternEvaluationCriteriaService.Delete;
using SWD.NextIntern.Service.Services.InternEvaluationCriteriaService.GetAll;
using SWD.NextIntern.Service.Services.InternEvaluationCriteriaService.GetById;
using SWD.NextIntern.Service.Services.InternEvaluationCriteriaService.Update;

namespace SWD.NextIntern.API.Controllers.InternEvaluationCriteria
{
    [Route("api/v1/intern-evaluation-criteria")]
    [ApiController]
    [Authorize]
    public class InternEvaluationCriteriaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InternEvaluationCriteriaController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("create")]
        public async Task<ResponseObject<string>> CreateInternEvaluationCriteria([FromBody] CreateInternEvaluationCriteriaCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("all")]
        public async Task<ResponseObject<List<InternEvaluationCriteriaDto>>> GetAllInternEvaluationCriteria(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAllQuery(), cancellationToken);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ResponseObject<InternEvaluationCriteriaDto>> GetInternEvaluationCriteriaById(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetInternEvaluationCriteriaByIdQuery(id), cancellationToken);
            return result;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<ResponseObject<string>> DeleteInternEvaluationCriteria(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteInternEvaluationCriteriaCommand(id), cancellationToken);
            return result;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("update")]
        public async Task<ResponseObject<string>> UpdateInternEvaluationCriteria([FromBody] UpdateInternEvaluationCriteriaCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }
    }
}
