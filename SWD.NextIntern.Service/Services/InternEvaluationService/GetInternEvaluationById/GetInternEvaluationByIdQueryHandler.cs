using AutoMapper;
using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignService;
using System.Net;

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
            var internEvaluation = await _internEvaluationRepository.FindAsync(ie => ie.InternEvaluationId.ToString().Equals(request.Id) && ie.DeletedDate == null, cancellationToken);
            if (internEvaluation == null)
            {
                return new ResponseObject<InternEvaluationDto>(System.Net.HttpStatusCode.NotFound, $"Intern Evaluation with id {request.Id} does not exist!");
            }

            return new ResponseObject<InternEvaluationDto>(_mapper.Map<InternEvaluationDto>(internEvaluation), HttpStatusCode.OK, "success!");
        }
    }
}
