using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignQuestionResponseService.Create;
using SWD.NextIntern.Service.Services.CampaignQuestionResponseService.Delete;
using SWD.NextIntern.Service.Services.CampaignQuestionResponseService.GetId;
using SWD.NextIntern.Service.Services.CampaignQuestionResponseService.Update;
using SWD.NextIntern.Service.Services.CampaignQuestionResponseService.GetAll;
using SWD.NextIntern.Service.Services.CampaignQuestionResponseService;

namespace SWD.NextIntern.API.Controllers.CampaignQuestionResponse
{
    [Route("api/v1/campaign-question-response")]
    [ApiController]
    [Authorize]
    public class CampaignQuestionResponseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CampaignQuestionResponseController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("all")]
        public async Task<ResponseObject<List<CampaignQuestionResponseDto>>> GetAllCampaignQuestionResponses(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAllQuery(), cancellationToken);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ResponseObject<CampaignQuestionResponseDto?>> GetCampaignQuestionResponseById(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetCampaignQuestionResponseByIdQuery(id), cancellationToken);
            return result;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("create")]
        public async Task<ResponseObject<string>> CreateCampaignQuestionResponse([FromBody] CreateCampaignQuestionResponseCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<ResponseObject<string>> DeleteCampaignQuestionResponse(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteCampaignQuestionResponseCommand(id), cancellationToken);
            return result;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("update")]
        public async Task<ResponseObject<string>> UpdateCampaignQuestionResponse([FromBody] UpdateCampaignQuestionResponseCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }
    }
}
