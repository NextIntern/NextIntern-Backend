using MediatR;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Service.Common.ResponseType;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignService;
using SWD.NextIntern.Service.Services.CampaignService.Create;
using SWD.NextIntern.Service.Services.CampaignService.GetAll;
using SWD.NextIntern.Service.Services.CampaignService.GetById;

namespace SWD.NextIntern.API.Controllers.Campaign;
[ApiController]
[Route("api/v1/campaign")]
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

    [HttpPost("create")]
    public async Task<ActionResult<ResponseObject<string>>> CreateCampaign([FromBody] CreateCampaignCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(new JsonResponse<ResponseObject<string>>(result));
    }
}
