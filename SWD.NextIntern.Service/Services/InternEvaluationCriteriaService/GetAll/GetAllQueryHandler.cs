using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignService;
using SWD.NextIntern.Service.Services.InternEvaluationCriteriaService.GetAll;
using System.Net;

namespace SWD.NextIntern.Service.Services.IntInternEvaluationCriteriaService.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, ResponseObject<List<InternEvaluationCriteriaDto>>>
    {
        private readonly IInternEvaluationCriteriaRepository _internEvaluationCriteriaRepository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(IInternEvaluationCriteriaRepository internEvaluationCriteriaRepository, IMapper mapper)
        {
            _internEvaluationCriteriaRepository = internEvaluationCriteriaRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<List<InternEvaluationCriteriaDto>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<InternEvaluationCriterion> query) =>
            {
                return query
                .Include(x => x.InternEvaluation)
                .Include(x => x.FormCriteria)
                .Where(x => x.DeletedDate == null); ;
            };

            var ivaCriteria = await _internEvaluationCriteriaRepository.FindAllAsync(queryOptions, cancellationToken);
            var ivaCriteriaDtos = _mapper.Map<List<InternEvaluationCriteriaDto>>(ivaCriteria);
            return new ResponseObject<List<InternEvaluationCriteriaDto>>(ivaCriteriaDtos, HttpStatusCode.OK, "Success!"); ;
        }
    }
}
