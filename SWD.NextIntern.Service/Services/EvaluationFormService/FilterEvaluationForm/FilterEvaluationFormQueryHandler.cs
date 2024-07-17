using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;

namespace SWD.NextIntern.Service.Services.EvaluationFormService.FilterEvaluationForm
{
    public class FilterEvaluationFormQueryHandler : IRequestHandler<FilterEvaluationFormQuery, ResponseObject<PagedListResponse<EvaluationFormDto>>>
    {
        private readonly IEvaluationFormRepository _evaluationFormRepository;

        public FilterEvaluationFormQueryHandler(IEvaluationFormRepository evaluationFormRepository)
        {
            _evaluationFormRepository = evaluationFormRepository;
        }

        public async Task<ResponseObject<PagedListResponse<EvaluationFormDto>>> Handle(FilterEvaluationFormQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<EvaluationForm> query) =>
            {
                return query
                .Include(x => x.University)
                .Where(x => x.DeletedDate == null); ;
            };

            var forms = await _evaluationFormRepository.FindAllProjectToAsync<EvaluationFormDto>(ef => ef.DeletedDate == null, request.PageNo, request.PageSize, queryOptions, cancellationToken);

            var response = new PagedListResponse<EvaluationFormDto>
            {
                Items = (PagedList<EvaluationFormDto>)forms,
                TotalCount = forms.TotalCount,
                PageCount = forms.PageCount,
                PageNo = forms.PageNo,
                PageSize = forms.PageSize
            };

            return new ResponseObject<PagedListResponse<EvaluationFormDto>>(response, HttpStatusCode.OK, "Success!");
        }
    }
}
