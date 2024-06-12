using MediatR;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Service.Common.ResponseType;
using SWD.NextIntern.Service.Services.CampaignService;
using SWD.NextIntern.Service.Services.CampaignService.Create;
using SWD.NextIntern.Service.Services.CampaignService.GetAll;
using SWD.NextIntern.Service.Services.CampaignService.GetById;

namespace SWD.NextIntern.API.Controllers.CampaignService
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CampaignController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("campaigns")]
        public async Task<ActionResult<List<UniversityDto>>> GetAllCampaign(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAllQuery(), cancellationToken);
            return Ok(new JsonResponse<List<UniversityDto>>(result));
        }

        [HttpGet("campaign/{id}")]
        public async Task<ActionResult<UniversityDto>> GetCampaignById(string id,CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetCampaignByIdQuery(id), cancellationToken);
            if (result is null)
            {
                return BadRequest(new JsonResponse<string>($"Campaign voi {id} khong ton tai"));
            }
            return Ok(new JsonResponse<UniversityDto>(result));
        }

        [HttpPost("campaign")]
        public async Task<ActionResult<string>> CreateCampaign([FromBody] CreateCampaignCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

    }
}
