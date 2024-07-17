using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignEvaluationService;
using System.Net;

namespace SWD.NextIntern.Service.Services.CampaignService.FilterCampaign
{
    public class FilterCampaignQueryHandler : IRequestHandler<FilterCampaignQuery, ResponseObject<PagedListResponse<CampaignDto>>>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMapper _mapper;

        public FilterCampaignQueryHandler(ICampaignRepository campaignRepository, IMapper mapper)
        {
            _campaignRepository = campaignRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<PagedListResponse<CampaignDto>>> Handle(FilterCampaignQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<Campaign> query) =>
            {
                return query.Include(x => x.University);
            };

            var campaigns = await _campaignRepository.FindAllProjectToAsync<CampaignDto>(c => c.DeletedDate == null, request.PageNo, request.PageSize, queryOptions, cancellationToken);

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
