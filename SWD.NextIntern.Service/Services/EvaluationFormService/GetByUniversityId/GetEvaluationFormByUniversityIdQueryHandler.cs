using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.EvaluationFormService.GetByUniversityId
{
    internal class GetEvaluationFormByUniversityIdQueryHandler : IRequestHandler<GetEvaluationFormByUniversityIdQuery, ResponseObject<PagedListResponse<EvaluationFormDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IEvaluationFormRepository _evaluationFormRepository;
        private readonly IUniversityRepository _universityRepository;

        public GetEvaluationFormByUniversityIdQueryHandler(IEvaluationFormRepository evaluationFormRepository, IMapper mapper, IUniversityRepository universityRepository)
        {
            _evaluationFormRepository = evaluationFormRepository;
            _mapper = mapper;
            _universityRepository = universityRepository;
        }

        public async Task<ResponseObject<PagedListResponse<EvaluationFormDto>>> Handle(GetEvaluationFormByUniversityIdQuery request, CancellationToken cancellationToken)
        {
            var university = await _universityRepository.FindAsync(u => u.UniversityId.ToString().Equals(request.UniversityId) && u.DeletedDate == null, cancellationToken);

            if (university is null)
            {
                return new ResponseObject<PagedListResponse<EvaluationFormDto>>(HttpStatusCode.NotFound, $"University with id {request.UniversityId} does not exist!");
            }

            var queryOptions = (IQueryable<EvaluationForm> query) =>
            {
                return query
                .Include(x => x.FormCriteria)
                .Include(x => x.University);
            };

            var forms = await _evaluationFormRepository.FindAllProjectToAsync<EvaluationFormDto>(x => x.DeletedDate == null && x.UniversityId.ToString().Equals(request.UniversityId), request.PageNo, request.PageSize, queryOptions, cancellationToken);

            if (forms == null)
            {
                return new ResponseObject<PagedListResponse<EvaluationFormDto>>(HttpStatusCode.NotFound, $"Evaluation Form with University id {request.UniversityId} does not exist!");
            }

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
