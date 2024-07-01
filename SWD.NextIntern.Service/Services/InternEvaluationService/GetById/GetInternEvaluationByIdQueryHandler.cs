using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignService;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SWD.NextIntern.Service.Services.InternEvaluationService.GetInternEvaluationById
{
    public class GetInternEvaluationByIdQueryHandler : IRequestHandler<GetInternEvaluationByIdQuery, ResponseObject<InternEvaluationDto>>
    {
        private readonly IInternEvaluationRepository _internEvaluationRepository;
        private readonly IMapper _mapper;

        public GetInternEvaluationByIdQueryHandler(IInternEvaluationRepository internEvaluationRepository, IMapper mapper)
        {
            _internEvaluationRepository = internEvaluationRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<InternEvaluationDto>> Handle(GetInternEvaluationByIdQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<InternEvaluation> query) =>
            {
                return query
                .Include(x => x.Intern)
                .Where(x => x.DeletedDate == null
                && x.InternEvaluationId.ToString().Equals(request.Id));
            };

            var internEvaluation = await _internEvaluationRepository.FindAsync(queryOptions, cancellationToken);
            
            if (internEvaluation == null)
            {
                return new ResponseObject<InternEvaluationDto>(HttpStatusCode.NotFound, $"Intern Evaluation with id {request.Id} does not exist!");
            }

            return new ResponseObject<InternEvaluationDto>(_mapper.Map<InternEvaluationDto>(internEvaluation), HttpStatusCode.OK, "Success!");
        }
    }
}
