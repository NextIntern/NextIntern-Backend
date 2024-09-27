using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.InternService.GetByUniversityId;
using System.Net;

namespace SWD.NextIntern.Service.Services.InternService.GetByCampaignId
{
    public class GetInternByCampaignIdQueryHandler : IRequestHandler<GetInternByCampaignIdQuery, ResponseObject<PagedListResponse<InternDto>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICampaignRepository _campaignRepository;

        public GetInternByCampaignIdQueryHandler(IUserRepository userRepository, ICampaignRepository campaignRepository)
        {
            _userRepository = userRepository;
            _campaignRepository = campaignRepository;
        }

        public async Task<ResponseObject<PagedListResponse<InternDto>>> Handle(GetInternByCampaignIdQuery request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignRepository.FindAsync(u => u.CampaignId.ToString().Equals(request.CampaignId) && u.DeletedDate == null, cancellationToken);

            if (campaign is null)
            {
                return new ResponseObject<PagedListResponse<InternDto>>(HttpStatusCode.NotFound, $"Campaign with id {request.CampaignId} does not exist!");
            }

            var queryOptions = (IQueryable<User> query) =>
            {
                return query
                .Where(x => x.DeletedDate == null
                && x.CampaignId.ToString().Equals(request.CampaignId))
                .Include(x => x.Campaign)
                .Include(x => x.Mentor);
            };

            var interns = await _userRepository.FindAllProjectToAsync<InternDto>(request.PageNo, request.PageSize, queryOptions, cancellationToken);

            if (interns == null)
            {
                return new ResponseObject<PagedListResponse<InternDto>>(HttpStatusCode.NotFound, $"Intern with Campaign id {request.CampaignId} does not exist!");
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
