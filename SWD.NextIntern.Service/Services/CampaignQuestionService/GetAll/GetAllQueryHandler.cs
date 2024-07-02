using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;

namespace SWD.NextIntern.Service.Services.CampaignQuestionService.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, ResponseObject<List<CampaignQuestionDto>>>
    {
        private readonly ICampaignQuestionRepository _campaignQuestionRepository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(ICampaignQuestionRepository campaignQuestionRepository, IMapper mapper)
        {
            _campaignQuestionRepository = campaignQuestionRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<List<CampaignQuestionDto>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<CampaignQuestion> query) =>
            {
                return query
                .Include(x => x.Campaign)
                .Where(x => x.DeletedDate == null); ;
            };

            var forms = await _campaignQuestionRepository.FindAllAsync(queryOptions, cancellationToken);
            var formDtos = _mapper.Map<List<CampaignQuestionDto>>(forms);
            return new ResponseObject<List<CampaignQuestionDto>>(formDtos, HttpStatusCode.OK, "Success!"); ;
        }
    }
}
