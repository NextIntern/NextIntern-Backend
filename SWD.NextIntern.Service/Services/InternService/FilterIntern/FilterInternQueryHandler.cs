using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.InternEvaluationService;
using System.Net;

namespace SWD.NextIntern.Service.Services.InternService.FilterIntern
{
    public class FilterInternQueryHandler : IRequestHandler<FilterInternQuery, ResponseObject<PagedListResponse<InternDto>>>
    {
        private readonly IUserRepository _userRepository;

        public FilterInternQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResponseObject<PagedListResponse<InternDto>>> Handle(FilterInternQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<User> query) =>
            {
                return query
                .Include(x => x.Campaign)
                .Include(x => x.Mentor)
                .Include(x => x.Role)
                .Where(x => x.DeletedDate == null); ;
            };

            var interns = await _userRepository.FindAllProjectToAsync<InternDto>(u => u.DeletedDate == null && u.Role.RoleName.ToLower().Equals("user"), request.PageNo, request.PageSize, queryOptions, cancellationToken);

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
