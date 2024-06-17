﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.UniversityService;
using SWD.NextIntern.Service.Services.UniversityService.GetById;
using SWD.NextIntern.Service.Services.UniversityService.Create;
using SWD.NextIntern.Service.Services.UniversityService.Update;
using SWD.NextIntern.Service.Services.UniversityService.Delete;
using SWD.NextIntern.Service.Services.UniversityService.Create;
using SWD.NextIntern.Service.Services.UniversityService.GetAll;

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
        public async Task<ResponseObject<UniversityDto>> GetUniversityById(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetUniversityByIdQuery(id), cancellationToken);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ResponseObject<string>> DeleteUniversity(string id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteUniversityCommand(id), cancellationToken);
            return result;
        }

        [HttpPut("update")]
        public async Task<ResponseObject<string>> UpdateUniversity([FromBody] UpdateUniversityCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }
    }
}