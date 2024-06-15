using MediatR;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Service.Common.ResponseType;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignService;
using SWD.NextIntern.Service.Services.CampaignService.Create;
using SWD.NextIntern.Service.Services.CampaignService.GetAll;
using SWD.NextIntern.Service.Services.CampaignService.GetById;

namespace SWD.NextIntern.API.Controllers.CampaignService
{
    [Route("api/v1/campaign")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CampaignController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

    [HttpGet("all")]
    public async Task<ActionResult<ResponseObject<List<CampaignDto>>>> GetAllCampaign(CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetAllQuery(), cancellationToken);
        return Ok(new JsonResponse<ResponseObject<List<CampaignDto>>>(result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseObject<CampaignDto>>> GetCampaignById(string id, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetCampaignByIdQuery(id), cancellationToken);
        if (result is null)
        {
            var result = await _mediator.Send(new GetAllQuery(), cancellationToken);
            return result;
        }
    }

    [HttpPost("create")]
    public async Task<ResponseObject<string>> CreateCampaign([FromBody] CreateCampaignCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ResponseObject<string>> DeleteCampaign(string id, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new DeleteCampaignCommand(id), cancellationToken);
        return result;
    }

    [HttpPut("")]
    public async Task<ResponseObject<string>> UpdateCampaign([FromBody] UpdateCampaignCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result;
    }    
  }
}
