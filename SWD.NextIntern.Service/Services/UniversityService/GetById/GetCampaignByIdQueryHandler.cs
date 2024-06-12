using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.Services.UniversityService.GetById;

namespace SWD.NextIntern.Service.Services.UniversityService.GetById
{
    public class GetUniversityByIdQueryHandler : IRequestHandler<GetUniversityByIdQuery, UniversityDto?>
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IMapper _mapper;

        public GetUniversityByIdQueryHandler(IMapper mapper, IUniversityRepository universityRepository)
        {
            _mapper = mapper;
            _universityRepository = universityRepository;
        }

        public async Task<UniversityDto?> Handle(GetUniversityByIdQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<University> query) =>
            {
                return query.Include(x => x.UniversityName);
            };

            var university = await _universityRepository.FindAsync(c => c.UniversityId.ToString().Equals(request.Id), queryOptions, cancellationToken);
            if (university is null)
            {
                return null;
            }
            return _mapper.Map<UniversityDto>(university);
        }
    }
}
