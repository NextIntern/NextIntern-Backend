
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Linq.Expressions;
using System.Net;

namespace SWD.NextIntern.Service.Services.FormCriteriaService.GetById
{
    public class GetFormCriteriaByIdQueryHandler : IRequestHandler<GetFormCriteriaByIdQuery, ResponseObject<FormCriteriaDto?>>
    {
        private readonly IFormCriteriaRepository _formCriteriaRepository;
        private readonly IMapper _mapper;

        public GetFormCriteriaByIdQueryHandler(IFormCriteriaRepository formCriteriaRepository, IMapper mapper)
        {
            _formCriteriaRepository = formCriteriaRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<FormCriteriaDto?>> Handle(GetFormCriteriaByIdQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<FormCriterion, bool>> queryFilter = (FormCriterion f) => f.FormCriteriaId.ToString().Equals(request.Id) && f.DeletedDate == null;

            var queryOptions = (IQueryable<FormCriterion> query) =>
            {
                return query
                .Include(x => x.EvaluationForm)
                    .ThenInclude(x => x.University)
                .Where(x => x.DeletedDate == null);
            };

            var form = await _formCriteriaRepository.FindAsync(queryFilter, queryOptions, cancellationToken);

            if (form == null)
            {
                return new ResponseObject<FormCriteriaDto?>(HttpStatusCode.NotFound, $"Form Criteria with id {request.Id}does not exist!");
            }

            return new ResponseObject<FormCriteriaDto?>(_mapper.Map<FormCriteriaDto>(form), HttpStatusCode.OK, "Success!");
        }
    }
}
