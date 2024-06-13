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
    public class UniversityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UniversityController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{id}")]
        public async Task<ResponseObject<UniversityDto?>> GetCampaignById(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetUniversityByIdQuery(id), cancellationToken);
            return result;
        }
    }
}
