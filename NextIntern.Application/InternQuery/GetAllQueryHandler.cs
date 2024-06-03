using MediatR;
using NextIntern.Domain.IRepositories;

namespace NextIntern.Application.InternQuery
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, List<NextIntern.Domain.Entities.Intern>>
    {
        private readonly IInternRepository _repo;

        public GetAllQueryHandler(IInternRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Domain.Entities.Intern>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await _repo.FindAllAsync(cancellationToken);
        }
    }
}
