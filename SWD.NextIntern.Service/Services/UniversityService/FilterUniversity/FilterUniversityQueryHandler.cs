using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.InternEvaluationService;
using System.Net;

namespace SWD.NextIntern.Service.Services.UniversityService.FilterUniversity
{
    public class FilterUniversityQueryHandler : IRequestHandler<FilterUniversityQuery, ResponseObject<PagedListResponse<UniversityDto>>>
    {
        private readonly IUniversityRepository _universityRepository;

        public FilterUniversityQueryHandler(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        public async Task<ResponseObject<PagedListResponse<UniversityDto>>> Handle(FilterUniversityQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<University> query) =>
            {
                return query
                .Include(x => x.Campaigns)
                .Where(x => x.DeletedDate == null); ;
            };

            var universities = await _universityRepository.FindAllProjectToAsync<UniversityDto>(u => u.DeletedDate == null, request.PageNo, request.PageSize, queryOptions, cancellationToken);

            var response = new PagedListResponse<UniversityDto>
            {
                Items = (PagedList<UniversityDto>)universities,
                TotalCount = universities.TotalCount,
                PageCount = universities.PageCount,
                PageNo = universities.PageNo,
                PageSize = universities.PageSize
            };

            return new ResponseObject<PagedListResponse<UniversityDto>>(response, HttpStatusCode.OK, "Success!");
        }
    }
}
