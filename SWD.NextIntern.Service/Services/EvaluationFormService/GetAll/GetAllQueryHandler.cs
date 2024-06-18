using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignService;
using System.Net;

namespace SWD.NextIntern.Service.Services.EvaluationFormService.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, ResponseObject<List<EvaluationFormDto>>>
    {
        private readonly IEvaluationFormRepository _evaluationFormRepository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(IEvaluationFormRepository evaluationFormRepository, IMapper mapper)
        {
            _evaluationFormRepository = evaluationFormRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<List<EvaluationFormDto>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<EvaluationForm> query) =>
            {
                return query
                .Include(x => x.University)
                .Where(x => x.DeletedDate == null); ;
            };

            var forms = await _evaluationFormRepository.FindAllAsync(queryOptions, cancellationToken);
            var formDtos = _mapper.Map<List<EvaluationFormDto>>(forms);
            return new ResponseObject<List<EvaluationFormDto>>(formDtos, HttpStatusCode.OK, "Success!"); ;
        }
    }
}
