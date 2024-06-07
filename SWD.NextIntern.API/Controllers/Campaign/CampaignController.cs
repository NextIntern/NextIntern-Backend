using MediatR;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Service.Common.ResponseType;
using SWD.NextIntern.Service.Services.CampaignService;
using SWD.NextIntern.Service.Services.CampaignService.Create;
using SWD.NextIntern.Service.Services.CampaignService.Delete;
using SWD.NextIntern.Service.Services.CampaignService.GetAll;
using SWD.NextIntern.Service.Services.CampaignService.GetById;
using SWD.NextIntern.Service.Services.CampaignService.Update;

namespace SWD.NextIntern.API.Controllers.CampaignService
{
    [Route("api/campaign")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CampaignController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<CampaignDto>>> GetAllCampaign(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAllQuery(), cancellationToken);
            return Ok(new JsonResponse<List<CampaignDto>>(result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CampaignDto>> GetCampaignById(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetCampaignByIdQuery(id), cancellationToken);
            if (result is null)
            {
                return BadRequest(new JsonResponse<string>($"Campaign voi {id} khong ton tai"));
            }
            return Ok(new JsonResponse<CampaignDto>(result));
        }

        [HttpPost("new")]
        public async Task<ActionResult<string>> CreateCampaign([FromBody] CreateCampaignCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteCampaign(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteCampaignCommand(id), cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        [HttpPut("")]
        public async Task<ActionResult<string>> UpdateCampaign([FromBody] UpdateCampaignCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }
    }
}
