﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.EvaluationFormService.GetAll;
using Microsoft.AspNetCore.Authorization;
using SWD.NextIntern.Service.Services.EvaluationFormService.GetById;
using SWD.NextIntern.Service.Services.EvaluationFormService.Create;
using SWD.NextIntern.Service.Services.EvaluationFormService.Delete;
using SWD.NextIntern.Service.Services.EvaluationFormService.Update;
using SWD.NextIntern.Service.Services.CampaignEvaluationService.FilterCampaignEvaluation;
using SWD.NextIntern.Service.Services.EvaluationFormService.FilterEvaluationForm;
using SWD.NextIntern.Service.Services.EvaluationFormService.GetByUniversityId;
namespace SWD.NextIntern.API.Controllers.EvaluationForm
{
    [ApiController]
    [Authorize(Policy = "AdminPolicy")]
    [Route("api/v1/evaluation-form")]
    public class EvaluationFormController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EvaluationFormController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        //[HttpGet("all")]
        //public async Task<ResponseObject<List<EvaluationFormDto>>> GetAllEvaluationForm(CancellationToken cancellationToken = default)
        //{
        //        var result = await _mediator.Send(new GetAllQuery(), cancellationToken);
        //        return result;
        //}

        [HttpGet("all")]
        public async Task<ResponseObject<PagedListResponse<EvaluationFormDto>>> GetAllEvaluationForm(int pageNo = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new FilterEvaluationFormQuery(pageSize, pageNo), cancellationToken);
            return result;
        }

        [HttpGet("university/{id}")]
        public async Task<ResponseObject<PagedListResponse<EvaluationFormDto>>> GetEvaluationFormByUniversityId(string id, int pageNo = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetEvaluationFormByUniversityIdQuery(id, pageNo, pageSize), cancellationToken);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ResponseObject<EvaluationFormDto?>> GetEvaluationFormById(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetEvaluationFormByIdQuery(id), cancellationToken);
            return result;
        }

        [HttpPost("create")]
        public async Task<ResponseObject<string>> CreateEvaluationForm([FromBody] CreateEvaluationFormCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ResponseObject<string>> DeleteEvaluationForm(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteEvaluationFormCommand(id), cancellationToken);
            return result;
        }

        [HttpPut("update")]
        public async Task<ResponseObject<string>> UpdateEvaluationForm([FromBody] UpdateEvaluationFormCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }
    }
}
