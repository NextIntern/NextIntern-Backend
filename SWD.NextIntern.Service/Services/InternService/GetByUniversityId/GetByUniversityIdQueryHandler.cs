using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.InternEvaluationService;
using System.Net;

namespace SWD.NextIntern.Service.Services.InternService.GetByUniversityId
{
    public class GetByUniversityIdQueryHandler : IRequestHandler<GetByUniversityIdQuery, ResponseObject<PagedListResponse<InternDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUniversityRepository _universityRepository;

        public GetByUniversityIdQueryHandler(IUserRepository userRepository, IMapper mapper, IUniversityRepository universityRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _universityRepository = universityRepository;
        }

        public async Task<ResponseObject<PagedListResponse<InternDto>>> Handle(GetByUniversityIdQuery request, CancellationToken cancellationToken)
        {
            var university = await _universityRepository.FindAsync(u => u.UniversityId.ToString().Equals(request.UniversityId) && u.DeletedDate == null, cancellationToken);

            if (university is null)
            {
                return new ResponseObject<PagedListResponse<InternDto>>(HttpStatusCode.NotFound, $"University with id {request.UniversityId} does not exist!");
            }

            var queryOptions = (IQueryable<User> query) =>
            {
                return query
                .Where(x => x.DeletedDate == null
                && x.UniversityId.ToString().Equals(request.UniversityId))
                .Include(x => x.Campaign)
                .Include(x => x.Mentor);
            };

            var interns = await _userRepository.FindAllProjectToAsync<InternDto>(request.PageNo, request.PageSize, queryOptions, cancellationToken);

            if (interns == null)
            {
                return new ResponseObject<PagedListResponse<InternDto>>(HttpStatusCode.NotFound, $"Intern with University id {request.UniversityId} does not exist!");
            }

            var response = new PagedListResponse<InternDto>
            {
                Items = (PagedList<InternDto>)interns,
                TotalCount = interns.TotalCount,
                PageCount = interns.PageCount,
                PageNo = interns.PageNo,
                PageSize = interns.PageSize
            };

            return new ResponseObject<PagedListResponse<InternDto>>(response, HttpStatusCode.OK, "Success!");
        }
    }
}
