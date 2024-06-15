
using AutoMapper;
using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Linq.Expressions;
using System.Net;

namespace SWD.NextIntern.Service.Services.EvaluationFormService.GetById
{
    public class GetEvaluationFormByIdQueryHandler : IRequestHandler<GetEvaluationFormByIdQuery, ResponseObject<EvaluationFormDto?>>
    {
        private readonly IEvaluationFormRepository _evaluationFormRepository;
        private readonly IMapper _mapper;

        public GetEvaluationFormByIdQueryHandler(IEvaluationFormRepository evaluationFormRepository, IMapper mapper)
        {
            _evaluationFormRepository = evaluationFormRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<EvaluationFormDto?>> Handle(GetEvaluationFormByIdQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<EvaluationForm, bool>> queryFilter = (EvaluationForm f) => f.EvaluationFormId.ToString().Equals(request.Id) && f.DeletedDate == null;

            var form = await _evaluationFormRepository.FindAsync(queryFilter, cancellationToken);

            if (form == null)
            {
                return new ResponseObject<EvaluationFormDto?>(HttpStatusCode.NotFound, $"Evaluation Form with id {request.Id} doest not exist!");
            }

            return new ResponseObject<EvaluationFormDto?>(_mapper.Map<EvaluationFormDto>(form), HttpStatusCode.OK, "success!");
        }
    }
}
