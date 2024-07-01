using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignEvaluationService.GetAll
{
    public class GetAllCampaignEvaluationQueryHandler : IRequestHandler<GetAllCampaignEvaluationQuery, ResponseObject<List<CampaignEvaluationDto>>>
    {
        private readonly ICampaignEvaluationRepository _campaignEvaluationRepository;
        private readonly IMapper _mapper;

        public GetAllCampaignEvaluationQueryHandler(ICampaignEvaluationRepository campaignEvaluationRepository, IMapper mapper)
        {
            _campaignEvaluationRepository = campaignEvaluationRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<List<CampaignEvaluationDto>>> Handle(GetAllCampaignEvaluationQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<CampaignEvaluation> query) =>
            {
                return query.Include(x => x.InternEvaluations)
                            .Include(x => x.Campaign);
            };

            var campaignEvaluations = await _campaignEvaluationRepository.FindAllAsync(ce => ce.DeletedDate == null, queryOptions, cancellationToken);
            var campaignEvaluationDtos = _mapper.Map<List<CampaignEvaluationDto>>(campaignEvaluations);
            return new ResponseObject<List<CampaignEvaluationDto>>(campaignEvaluationDtos, HttpStatusCode.OK, "Success!");
        }
    }
}
