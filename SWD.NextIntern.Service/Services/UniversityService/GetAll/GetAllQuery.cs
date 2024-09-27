using MediatR;
using Microsoft.AspNetCore.Authorization;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.UniversityService.GetAll
{
    public class GetAllQuery : IRequest<ResponseObject<List<UniversityDto>>>, IQuery
    {
    }
}
