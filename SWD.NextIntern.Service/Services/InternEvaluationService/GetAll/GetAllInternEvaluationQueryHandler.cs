using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignService;
using System.Net;

namespace SWD.NextIntern.Service.Services.InternEvaluationService.GetAllInternEvaluation
{
    public class GetAllInternEvaluationQueryHandler : IRequestHandler<GetAllInternEvaluationQuery, ResponseObject<List<InternEvaluationDto>>>
    {
        private readonly IInternEvaluationRepository _internEvaluationRepository;
        private readonly IMapper _mapper;

        public GetAllInternEvaluationQueryHandler(IInternEvaluationRepository internEvaluationRepository, IMapper mapper)
        {
            _internEvaluationRepository = internEvaluationRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<List<InternEvaluationDto>>> Handle(GetAllInternEvaluationQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<InternEvaluation> query) =>
            {
                return query
                .Include(x => x.Intern)
                .Where(x => x.DeletedDate == null); ;
            };

            var internEvaluations = await _internEvaluationRepository.FindAllAsync(queryOptions, cancellationToken);

            return new ResponseObject<List<InternEvaluationDto>>(_mapper.Map<List<InternEvaluationDto>>(internEvaluations), HttpStatusCode.OK, "Success!");
        }
    }
}
