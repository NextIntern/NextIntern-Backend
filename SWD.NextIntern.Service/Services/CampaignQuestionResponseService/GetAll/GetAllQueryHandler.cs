using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignQuestionResponseService.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, ResponseObject<List<CampaignQuestionResponseDto>>>
    {
        private readonly ICampaignQuestionResponseRepository _campaignQuestionResponseRepository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(ICampaignQuestionResponseRepository campaignQuestionResponseRepository, IMapper mapper)
        {
            _campaignQuestionResponseRepository = campaignQuestionResponseRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<List<CampaignQuestionResponseDto>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<CampaignQuestionResponse> query) =>
            {
                return query
                .Include(x => x.CampaignQuestion)
                .Where(x => x.DeletedDate == null); ;
            };

            var forms = await _campaignQuestionResponseRepository.FindAllAsync(queryOptions, cancellationToken);
            var formDtos = _mapper.Map<List<CampaignQuestionResponseDto>>(forms);
            return new ResponseObject<List<CampaignQuestionResponseDto>>(formDtos, HttpStatusCode.OK, "Success!"); ;
        }
    }
}
