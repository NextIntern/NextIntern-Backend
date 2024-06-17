using MediatR;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.FormCriteriaService.GetAll;
using Microsoft.AspNetCore.Authorization;
using SWD.NextIntern.Service.Services.FormCriteriaService.GetById;
using SWD.NextIntern.Service.Services.FormCriteriaService.Create;
using SWD.NextIntern.Service.Services.FormCriteriaService.Delete;
using SWD.NextIntern.Service.Services.FormCriteriaService.Update;
namespace SWD.NextIntern.API.Controllers.FormCriteria
{
    [ApiController]
    [Authorize]
    [Route("api/v1/form-criteria")]
    public class FormCriteriaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FormCriteriaController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("all")]
        public async Task<ResponseObject<List<FormCriteriaDto>>> GetAllFormCriteria(CancellationToken cancellationToken = default)
        {
                var result = await _mediator.Send(new GetAllQuery(), cancellationToken);
                return result;
        }

        [HttpGet("{id}")]
        public async Task<ResponseObject<FormCriteriaDto?>> GetFormCriteriaById(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetFormCriteriaByIdQuery(id), cancellationToken);
            return result;
        }

        [HttpPost("create")]
        public async Task<ResponseObject<string>> CreateFormCriteria([FromBody] CreateFormCriteriaCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ResponseObject<string>> DeleteFormCriteria(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteFormCriteriaCommand(id), cancellationToken);
            return result;
        }

        [HttpPut("update")]
        public async Task<ResponseObject<string>> UpdateFormCriteria([FromBody] UpdateFormCriteriaCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }
    }
}
