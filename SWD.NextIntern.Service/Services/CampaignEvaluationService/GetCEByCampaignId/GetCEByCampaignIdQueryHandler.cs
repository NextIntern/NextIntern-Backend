using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignEvaluationService.FilterCampaignEvaluation;
using System.Net;

namespace SWD.NextIntern.Service.Services.CampaignEvaluationService.GetCEByCampaignId
{
    public class GetCEByCampaignIdQueryHandler : IRequestHandler<GetCEByCampaignIdQuery, ResponseObject<PagedListResponse<CampaignEvaluationDto>>>
    {
        private readonly ICampaignEvaluationRepository _campaignEvaluationRepository;
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMapper _mapper;

        public GetCEByCampaignIdQueryHandler(ICampaignEvaluationRepository campaignEvaluationRepository, IMapper mapper, ICampaignRepository campaignRepository)
        {
            _campaignEvaluationRepository = campaignEvaluationRepository;
            _mapper = mapper;
            _campaignRepository = campaignRepository;
        }

        public async Task<ResponseObject<PagedListResponse<CampaignEvaluationDto>>> Handle(GetCEByCampaignIdQuery request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignRepository.FindAsync(c => c.CampaignId.ToString().Equals(request.CampaignId) && c.DeletedDate == null, cancellationToken);

            if (campaign is null)
            {
                return new ResponseObject<PagedListResponse<CampaignEvaluationDto>>(HttpStatusCode.NotFound, $"Campaign with id {request.CampaignId} does not exist!");
            }

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
