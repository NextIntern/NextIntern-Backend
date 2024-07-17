using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignService;
using System.Net;

namespace SWD.NextIntern.Service.Services.CampaignEvaluationService.FilterCampaignEvaluation
{
    public class FilterCampaignEvaluationQueryHandler : IRequestHandler<FilterCampaignEvaluationQuery, ResponseObject<PagedListResponse<CampaignEvaluationDto>>>
    {
        private readonly ICampaignEvaluationRepository _campaignEvaluationRepository;
        private readonly IMapper _mapper;

        public FilterCampaignEvaluationQueryHandler(ICampaignEvaluationRepository campaignEvaluationRepository, IMapper mapper)
        {
            _campaignEvaluationRepository = campaignEvaluationRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<PagedListResponse<CampaignEvaluationDto>>> Handle(FilterCampaignEvaluationQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<CampaignEvaluation> query) =>
            {
                return query.Include(x => x.InternEvaluations)
                            .Include(x => x.Campaign);
            };

            var campaignEvaluations = await _campaignEvaluationRepository.FindAllProjectToAsync<CampaignEvaluationDto>(ce => ce.DeletedDate == null, request.PageNo, request.PageSize, queryOptions, cancellationToken);
            var response = new PagedListResponse<CampaignEvaluationDto>
            {
                Items = (PagedList<CampaignEvaluationDto>)campaignEvaluations,
                TotalCount = campaignEvaluations.TotalCount,
                PageCount = campaignEvaluations.PageCount,
                PageNo = campaignEvaluations.PageNo,
                PageSize = campaignEvaluations.PageSize
            };
            return new ResponseObject<PagedListResponse<CampaignEvaluationDto>>(response, HttpStatusCode.OK, "Success!");
        }
    }
}
