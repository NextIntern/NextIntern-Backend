using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.DashboardService._5MostIntern;
using SWD.NextIntern.Service.Services.DashboardService.CountIntern;
using SWD.NextIntern.Service.Services.DashboardService.Items;
using SWD.NextIntern.Service.Services.DashboardService.MostIntern;

namespace SWD.NextIntern.API.Controllers.Dashboard
{
    [Route("api/v1/dashboard")]
    [ApiController]
    [Authorize(Policy = "AdminPolicy")]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("items")]
        public async Task<ResponseObject<List<ItemsDto>>> GetAllItems(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new ItemsQuery(), cancellationToken);
            return result;
        }

        [HttpGet("count-intern")]
        public async Task<ResponseObject<List<CountInternDto>>> CountIntern(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new CountInternQuery(), cancellationToken);
            return result;
        }

        [HttpGet("5-most-intern")]
        public async Task<ResponseObject<List<MostInternDto>>> GetMostIntern(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new MostInternQuery(), cancellationToken);
            return result;
        }
    }
}
