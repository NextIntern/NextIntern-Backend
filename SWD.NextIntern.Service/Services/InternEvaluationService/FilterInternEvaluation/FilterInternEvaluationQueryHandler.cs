using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;

namespace SWD.NextIntern.Service.Services.InternEvaluationService.FilterInternEvaluation
{
    public class FilterInternEvaluationQueryHandler : IRequestHandler<FilterInternEvaluationQuery, ResponseObject<PagedListResponse<InternEvaluationDto>>>
    {
        private readonly IInternEvaluationRepository _interEvaluationRepository;

        public FilterInternEvaluationQueryHandler(IInternEvaluationRepository interEvaluationRepository)
        {
            _interEvaluationRepository = interEvaluationRepository;
        }

        public async Task<ResponseObject<PagedListResponse<InternEvaluationDto>>> Handle(FilterInternEvaluationQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<InternEvaluation> query) =>
            {
                return query
                .Include(x => x.Intern)
                .Where(x => x.DeletedDate == null); ;
            };

            var internEvaluations = await _interEvaluationRepository.FindAllProjectToAsync<InternEvaluationDto>(ie => ie.DeletedDate == null, request.PageNo, request.PageSize, queryOptions, cancellationToken);

            var response = new PagedListResponse<InternEvaluationDto>
            {
                Items = (PagedList<InternEvaluationDto>)internEvaluations,
                TotalCount = internEvaluations.TotalCount,
                PageCount = internEvaluations.PageCount,
                PageNo = internEvaluations.PageNo,
                PageSize = internEvaluations.PageSize
            };

            return new ResponseObject<PagedListResponse<InternEvaluationDto>>(response, HttpStatusCode.OK, "Success!");
        }
    }
}
