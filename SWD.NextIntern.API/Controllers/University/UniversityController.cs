using MediatR;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignService.GetAll;
using SWD.NextIntern.Service.Services.CampaignService;
using SWD.NextIntern.Service.Services.CampaignService.GetById;
using SWD.NextIntern.Service.Services.UniversityService;
using SWD.NextIntern.Service.Services.UniversityService.GetById;

namespace SWD.NextIntern.API.Controllers.University
{
    [Route("api/v1/university")]
    [ApiController]
    [Authorize]
    public class UniversityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UniversityController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("create")]
        public async Task<ResponseObject<string>> CreateUniversity([FromBody] CreateUniversityCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }

        [HttpGet("all")]
        public async Task<ResponseObject<List<UniversityDto>>> GetAllUniversity(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAllQuery(), cancellationToken);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ResponseObject<UniversityDto?>> GetCampaignById(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetUniversityByIdQuery(id), cancellationToken);
            return result;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<ResponseObject<string>> DeleteUniversity(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteUniversityCommand(id), cancellationToken);
            return result;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("update")]
        public async Task<ResponseObject<string>> UpdateUniversity([FromBody] UpdateUniversityCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }
    }
}
