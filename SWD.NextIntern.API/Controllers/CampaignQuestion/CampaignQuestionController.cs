using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignQuestionService;
using SWD.NextIntern.Service.Services.CampaignQuestionService.Create;
using SWD.NextIntern.Service.Services.CampaignQuestionService.Delete;
using SWD.NextIntern.Service.Services.CampaignQuestionService.GetAll;
using SWD.NextIntern.Service.Services.CampaignQuestionService.GetId;
using SWD.NextIntern.Service.Services.CampaignQuestionService.Update;

namespace SWD.NextIntern.API.Controllers.CampaignQuestion
{
    [Route("api/v1/campaign-question")]
    [ApiController]
    [Authorize]
    public class CampaignQuestionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CampaignQuestionController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("all")]
        public async Task<ResponseObject<List<CampaignQuestionDto>>> GetAllCampaignQuestions(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAllQuery(), cancellationToken);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ResponseObject<CampaignQuestionDto?>> GetCampaignQuestionById(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetCampaignQuestionByIdQuery(id), cancellationToken);
            return result;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("create")]
        public async Task<ResponseObject<string>> CreateCampaignQuestion([FromBody] CreateCampaignQuestionCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<ResponseObject<string>> DeleteCampaignQuestion(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteCampaignQuestionCommand(id), cancellationToken);
            return result;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("update")]
        public async Task<ResponseObject<string>> UpdateCampaignQuestion([FromBody] UpdateCampaignQuestionCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }
    }
}
