using MediatR;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignEvaluationService.Create;
using SWD.NextIntern.Service.Services.CampaignService.GetAll;
using SWD.NextIntern.Service.Services.CampaignService;
using SWD.NextIntern.Service.Services.CampaignEvaluationService.GetAll;
using SWD.NextIntern.Service.Services.CampaignEvaluationService;

namespace SWD.NextIntern.API.Controllers.CampaignEvaluation
{
    [Route("api/v1/campaign-evaluation")]
    [ApiController]
    public class CampaignEvaluationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CampaignEvaluationController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("all")]
        public async Task<ResponseObject<List<CampaignEvaluationDto>>> GetAllCampaign(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAllCampaignEvaluationQuery(), cancellationToken);
            return result;
        }

        [HttpPost("create")]
        public async Task<ResponseObject<string>> CreateCampaignEvaluation([FromBody] CreateCampaignEvaluationCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }
    }
}
