using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;

namespace SWD.NextIntern.Service.Services.FormCriteriaService.FilterFormCriteria
{
    public class FilterFormCriteriaQueryHandler : IRequestHandler<FilterFormCriteriaQuery, ResponseObject<PagedListResponse<FormCriteriaDto>>>
    {
        private readonly IFormCriteriaRepository _formCriteriaRepository;

        public FilterFormCriteriaQueryHandler(IFormCriteriaRepository formCriteriaRepository)
        {
            _formCriteriaRepository = formCriteriaRepository;
        }

        public async Task<ResponseObject<PagedListResponse<FormCriteriaDto>>> Handle(FilterFormCriteriaQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<FormCriterion> query) =>
            {
                return query
                .Include(x => x.EvaluationForm)
                    .ThenInclude(x => x.University)
                .Where(x => x.DeletedDate == null);
            };

            var formCriterias = await _formCriteriaRepository.FindAllProjectToAsync<FormCriteriaDto>(fc => fc.DeletedDate == null, request.PageNo, request.PageSize, queryOptions, cancellationToken);

            var response = new PagedListResponse<FormCriteriaDto>
            {
                Items = (PagedList<FormCriteriaDto>)formCriterias,
                TotalCount = formCriterias.TotalCount,
                PageCount = formCriterias.PageCount,
                PageNo = formCriterias.PageNo,
                PageSize = formCriterias.PageSize
            };

            return new ResponseObject<PagedListResponse<FormCriteriaDto>>(response, HttpStatusCode.OK, "Success!");
        }
    }
}
