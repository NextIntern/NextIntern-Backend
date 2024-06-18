
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Linq.Expressions;
using System.Net;

namespace SWD.NextIntern.Service.Services.InternEvaluationCriteriaService.GetById
{
    public class GetInternEvaluationCriteriaByIdQueryHandler : IRequestHandler<GetInternEvaluationCriteriaByIdQuery, ResponseObject<InternEvaluationCriteriaDto?>>
    {
        private readonly IInternEvaluationCriteriaRepository _internEvaluationCriteriaRepository;
        private readonly IMapper _mapper;

        public GetInternEvaluationCriteriaByIdQueryHandler(IInternEvaluationCriteriaRepository internEvaluationCriteriaRepository, IMapper mapper)
        {
            _internEvaluationCriteriaRepository = internEvaluationCriteriaRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<InternEvaluationCriteriaDto?>> Handle(GetInternEvaluationCriteriaByIdQuery request, CancellationToken cancellationToken)
        {
            //Expression<Func<User, bool>> queryFilter = (User f) => f.UserId.ToString().Equals(request.Id) && f.DeletedDate == null;

            var queryOptions = (IQueryable<InternEvaluationCriterion> query) =>
            {
                return query
                .Include(x => x.InternEvaluation)
                .Include(x => x.FormCriteria)
                .Where(x => x.DeletedDate == null); ;
            };

            var ivaCriteria = await _internEvaluationCriteriaRepository.FindAsync(queryOptions, cancellationToken);

            if (ivaCriteria == null)
            {
                return new ResponseObject<InternEvaluationCriteriaDto?>(HttpStatusCode.NotFound, $"InternEvaluationCriteria with id {request.Id} does not exist!");
            }

            return new ResponseObject<InternEvaluationCriteriaDto?>(_mapper.Map<InternEvaluationCriteriaDto>(ivaCriteria), HttpStatusCode.OK, "Success!");
        }
    }
}
