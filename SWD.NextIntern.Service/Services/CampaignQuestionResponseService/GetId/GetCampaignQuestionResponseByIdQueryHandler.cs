using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignQuestionService.GetId;
using SWD.NextIntern.Service.Services.CampaignQuestionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace SWD.NextIntern.Service.Services.CampaignQuestionResponseService.GetId
{
    public class GetCampaignQuestionResponseByIdQueryHandler : IRequestHandler<GetCampaignQuestionResponseByIdQuery, ResponseObject<CampaignQuestionResponseDto>>
    {
        private readonly ICampaignQuestionResponseRepository _campaignQuestionResponseRepository;
        private readonly IMapper _mapper;

        public GetCampaignQuestionResponseByIdQueryHandler(IMapper mapper, ICampaignQuestionResponseRepository campaignQuestionResponseRepository)
        {
            _mapper = mapper;
            _campaignQuestionResponseRepository = campaignQuestionResponseRepository;
        }

        public async Task<ResponseObject<CampaignQuestionResponseDto>> Handle(GetCampaignQuestionResponseByIdQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<CampaignQuestionResponse> query) =>
            {
                return query.Include(x => x.CampaignQuestion);
            };

            var question = await _campaignQuestionResponseRepository.FindAsync(c => c.CampaignQuestionResponseId.ToString().Equals(request.Id) && c.DeletedDate == null, queryOptions, cancellationToken);
            if (question is null)
            {
                return new ResponseObject<CampaignQuestionResponseDto>(HttpStatusCode.NotFound, $"Campaign Question Response with id {request.Id} does not exist!");
            }
            return new ResponseObject<CampaignQuestionResponseDto>(_mapper.Map<CampaignQuestionResponseDto>(question), HttpStatusCode.OK, "Success!");
        }
    }
}
