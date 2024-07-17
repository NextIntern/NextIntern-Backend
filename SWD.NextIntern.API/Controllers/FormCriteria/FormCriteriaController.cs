using MediatR;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Service.DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using SWD.NextIntern.Service.Services.FormCriteriaService.GetById;
using SWD.NextIntern.Service.Services.FormCriteriaService.Create;
using SWD.NextIntern.Service.Services.FormCriteriaService.Delete;
using SWD.NextIntern.Service.Services.FormCriteriaService.Update;
using SWD.NextIntern.Service.Services.FormCriteriaService.FilterFormCriteria;
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

        //[HttpGet("all")]
        //public async Task<ResponseObject<List<FormCriteriaDto>>> GetAllFormCriteria(CancellationToken cancellationToken = default)
        //{
        //        var result = await _mediator.Send(new GetAllQuery(), cancellationToken);
        //        return result;
        //}

        [HttpGet("all")]
        public async Task<ResponseObject<PagedListResponse<FormCriteriaDto>>> GetAllFormCriteria(int pageNo = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new FilterFormCriteriaQuery(pageSize, pageNo), cancellationToken);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ResponseObject<FormCriteriaDto?>> GetFormCriteriaById(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetFormCriteriaByIdQuery(id), cancellationToken);
            return result;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("create")]
        public async Task<ResponseObject<string>> CreateFormCriteria([FromBody] CreateFormCriteriaCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<ResponseObject<string>> DeleteFormCriteria(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteFormCriteriaCommand(id), cancellationToken);
            return result;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("update")]
        public async Task<ResponseObject<string>> UpdateFormCriteria([FromBody] UpdateFormCriteriaCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }
    }
}
