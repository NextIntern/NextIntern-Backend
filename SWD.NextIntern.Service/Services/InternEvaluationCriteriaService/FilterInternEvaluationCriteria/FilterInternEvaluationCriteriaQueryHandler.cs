using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;

namespace SWD.NextIntern.Service.Services.InternEvaluationCriteriaService.FilterInternEvaluationCriteria
{
    public class FilterInternEvaluationCriteriaQueryHandler : IRequestHandler<FilterInternEvaluationCriteriaQuery, ResponseObject<PagedListResponse<InternEvaluationCriteriaDto>>>
    {
        private readonly IInternEvaluationCriteriaRepository _internEvaluationCriteriaRepository;

        public FilterInternEvaluationCriteriaQueryHandler(IInternEvaluationCriteriaRepository internEvaluationCriteriaRepository)
        {
            _internEvaluationCriteriaRepository = internEvaluationCriteriaRepository;
        }

        public async Task<ResponseObject<PagedListResponse<InternEvaluationCriteriaDto>>> Handle(FilterInternEvaluationCriteriaQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<InternEvaluationCriterion> query) =>
            {
                return query
                .Include(x => x.InternEvaluation)
                .Include(x => x.FormCriteria)
                .Where(x => x.DeletedDate == null); ;
            };

            var ivaCriteria = await _internEvaluationCriteriaRepository.FindAllProjectToAsync<InternEvaluationCriteriaDto>(iec => iec.DeletedDate == null, request.PageNo, request.PageSize, queryOptions, cancellationToken);

            var response = new PagedListResponse<InternEvaluationCriteriaDto>
            {
                Items = (PagedList<InternEvaluationCriteriaDto>)ivaCriteria,
                TotalCount = ivaCriteria.TotalCount,
                PageCount = ivaCriteria.PageCount,
                PageNo = ivaCriteria.PageNo,
                PageSize = ivaCriteria.PageSize
            };

            return new ResponseObject<PagedListResponse<InternEvaluationCriteriaDto>>(response, HttpStatusCode.OK, "Success!");
        }
    }
}
