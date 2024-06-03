using MediatR;
using NextIntern.Application.Common.Interfaces;
using NextIntern.Domain.Entities;

namespace NextIntern.Application.Intern
{
    public class GetAllQuery : IRequest<List<NextIntern.Domain.Entities.Intern>>, IQuerry
    {
    }
}
