using MediatR;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignEvaluationService.Create;
using SWD.NextIntern.Service.Services.CampaignEvaluationService.GetAll;
using SWD.NextIntern.Service.Services.CampaignEvaluationService;
using SWD.NextIntern.Service.Services.CampaignEvaluationService.GetById;
using SWD.NextIntern.Service.Services.CampaignEvaluationService.Delete;
using SWD.NextIntern.Service.Services.CampaignEvaluationService.Update;
using Microsoft.AspNetCore.Authorization;
using SWD.NextIntern.Service.Services.CampaignEvaluationService.FilterCampaignEvaluation;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Repository.Repositories;

namespace SWD.NextIntern.API.Controllers.CampaignEvaluation
{
    [Route("api/v1/campaign-evaluation")]
    [ApiController]
    [Authorize(Policy = "AdminPolicy")]
    public class CampaignEvaluationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CampaignEvaluationController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("all")]
        public async Task<ResponseObject<PagedListResponse<CampaignEvaluationDto>>> GetAllCampaign(int pageNo = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new FilterCampaignEvaluationQuery(pageSize, pageNo), cancellationToken);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ResponseObject<CampaignEvaluationDto>> GetCampaignEvaluationById(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetCampaignEvaluationByIdQuery(id), cancellationToken);
            return result;
        }

        [HttpPost("create")]
        public async Task<ResponseObject<string>> CreateCampaignEvaluation([FromBody] CreateCampaignEvaluationCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ResponseObject<string>> DeleteCampaignEvaluation(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteCampaignEvaluationCommand(id), cancellationToken);
            return result;
        }

        [HttpPut("update")]
        public async Task<ResponseObject<string>> UpdateCampaign([FromBody] UpdateCampaignEvaluationCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }
    }
}
