using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignService;
using SWD.NextIntern.Service.Services.CampaignService.Create;
using SWD.NextIntern.Service.Services.CampaignService.Delete;
using SWD.NextIntern.Service.Services.CampaignService.FilterCampaign;
using SWD.NextIntern.Service.Services.CampaignService.GetAll;
using SWD.NextIntern.Service.Services.CampaignService.GetById;
using SWD.NextIntern.Service.Services.CampaignService.GetByUniversityId;
using SWD.NextIntern.Service.Services.CampaignService.Update;

namespace SWD.NextIntern.API.Controllers.Campaign
{
    [Route("api/v1/campaign")]
    [ApiController]
    [Authorize]
    public class CampaignController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CampaignController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        //[HttpGet("all")]
        //public async Task<ResponseObject<List<CampaignDto>>> GetAllCampaign(CancellationToken cancellationToken = default)
        //{
        //    var result = await _mediator.Send(new GetAllQuery(), cancellationToken);
        //    return result;
        //}

        [HttpGet("all")]
        public async Task<ResponseObject<PagedListResponse<CampaignDto>>> GetAllCampaignWithFilter(int pageSize = 10, int pageNo = 1, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new FilterCampaignQuery(pageSize, pageNo), cancellationToken);
            return result;
        }

        [HttpGet("university/{id}")]
        public async Task<ResponseObject<PagedListResponse<CampaignDto>>> GetCampaignByUniversityId(string id, int pageSize = 10, int pageNo = 1, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetCampaignByUniversityIdQuery(pageSize, pageNo, id), cancellationToken);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ResponseObject<CampaignDto?>> GetCampaignById(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetCampaignByIdQuery(id), cancellationToken);
            return result;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("create")]
        public async Task<ResponseObject<string>> CreateCampaign([FromBody] CreateCampaignCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<ResponseObject<string>> DeleteCampaign(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteCampaignCommand(id), cancellationToken);
            return result;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("update")]
        public async Task<ResponseObject<string>> UpdateCampaign([FromBody] UpdateCampaignCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }
    }
}
