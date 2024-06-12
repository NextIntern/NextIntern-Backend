using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;

namespace SWD.NextIntern.Service.Services.UniversityService.GetAll
{
    public class GetAllQuery : IRequest<List<UniversityDto>>, IQuery
    {
    }
}
