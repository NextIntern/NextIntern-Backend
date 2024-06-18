using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignService;
using System.Net;

namespace SWD.NextIntern.Service.Services.FormCriteriaService.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, ResponseObject<List<FormCriteriaDto>>>
    {
        private readonly IFormCriteriaRepository _formCriteriaRepository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(IFormCriteriaRepository formCriteriaRepository, IMapper mapper)
        {
            _formCriteriaRepository = formCriteriaRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<List<FormCriteriaDto>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<FormCriterion> query) =>
            {
                return query
                .Include(x => x.EvaluationForm)
                .Where(x => x.DeletedDate == null);
            };

            var forms = await _formCriteriaRepository.FindAllAsync(queryOptions, cancellationToken);
            var formDtos = _mapper.Map<List<FormCriteriaDto>>(forms);
            return new ResponseObject<List<FormCriteriaDto>>(formDtos, HttpStatusCode.OK, "Success!"); ;
        }
    }
}
