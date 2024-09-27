using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.RoleService;
using SWD.NextIntern.Service.Services.RoleService.GetAll;

namespace SWD.NextIntern.API.Controllers.Roles
{
    [Authorize(Policy = "AdminPolicy")]
    [ApiController]
    [Route("api/v1/role")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("all")]
        public async Task<ResponseObject<List<RoleDto>>> GetAllRole(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAllQuery(), cancellationToken);
            return result;
        }
    }
}
