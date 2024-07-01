using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignService;
using System.Net;

namespace SWD.NextIntern.Service.Services.CampaignEvaluationService.GetById
{
    public class GetCampaignEvaluationByIdQueryHandler : IRequestHandler<GetCampaignEvaluationByIdQuery, ResponseObject<CampaignEvaluationDto>>
    {
        private readonly ICampaignEvaluationRepository _campaignEvaluationRepository;
        private readonly IMapper _mapper;

        public GetCampaignEvaluationByIdQueryHandler(ICampaignEvaluationRepository campaignEvaluationRepository, IMapper mapper)
        {
            _campaignEvaluationRepository = campaignEvaluationRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<CampaignEvaluationDto>> Handle(GetCampaignEvaluationByIdQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<CampaignEvaluation> query) =>
            {
                return query.Include(x => x.InternEvaluations)
                            .Include(x => x.Campaign);
            };

            var campaignEvaluation = await _campaignEvaluationRepository.FindAsync(ce => ce.CampaignEvaluationId.ToString().Equals(request.Id) && ce.DeletedDate == null, queryOptions, cancellationToken);

            if (campaignEvaluation is null)
            {
                return new ResponseObject<CampaignEvaluationDto>(HttpStatusCode.NotFound, $"Campaign Evaluation with id {request.Id} does not exist");
            }

            return new ResponseObject<CampaignEvaluationDto>(_mapper.Map<CampaignEvaluationDto>(campaignEvaluation), HttpStatusCode.OK, "success!");
        }
    }
}
