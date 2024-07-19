using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;

namespace SWD.NextIntern.Service.Services.CampaignService.GetByUniversityId
{
    public class GetCampaignByUniversityIdQueryHandler : IRequestHandler<GetCampaignByUniversityIdQuery, ResponseObject<PagedListResponse<CampaignDto>>>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMapper _mapper;
        private readonly IUniversityRepository _universityRepository;

        public GetCampaignByUniversityIdQueryHandler(ICampaignRepository campaignRepository, IMapper mapper, IUniversityRepository universityRepository)
        {
            _campaignRepository = campaignRepository;
            _mapper = mapper;
            _universityRepository = universityRepository;
        }

        public async Task<ResponseObject<PagedListResponse<CampaignDto>>> Handle(GetCampaignByUniversityIdQuery request, CancellationToken cancellationToken)
        {
            var university = await _universityRepository.FindAsync(u => u.UniversityId.ToString().Equals(request.UniversityId) && u.DeletedDate == null, cancellationToken);

            if (university is null)
            {
                return new ResponseObject<PagedListResponse<CampaignDto>>(HttpStatusCode.NotFound, $"University with id {request.UniversityId} does not exist!");
            }

            var queryOptions = (IQueryable<Campaign> query) =>
            {
                return query.Include(x => x.University);
            };

            var campaigns = await _campaignRepository.FindAllProjectToAsync<CampaignDto>(c => c.DeletedDate == null && c.UniversityId.ToString().Equals(request.UniversityId), request.PageNo, request.PageSize, queryOptions, cancellationToken);

            var response = new PagedListResponse<CampaignDto>
            {
                Items = (PagedList<CampaignDto>)campaigns,
                TotalCount = campaigns.TotalCount,
                PageCount = campaigns.PageCount,
                PageNo = campaigns.PageNo,
                PageSize = campaigns.PageSize
            };

            return new ResponseObject<PagedListResponse<CampaignDto>>(response, HttpStatusCode.OK, "Success!");
        }
    }
}
