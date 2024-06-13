using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;

namespace SWD.NextIntern.Service.Services.CampaignService.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, ResponseObject<List<CampaignDto>>>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(ICampaignRepository campaignRepository, IMapper mapper)
        {
            _campaignRepository = campaignRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<List<CampaignDto>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<Campaign> query) =>
            {
                return query
                .Include(x => x.University)
                .Where(x => x.DeletedDate == null);
            };

            var campaigns = await _campaignRepository.FindAllAsync(queryOptions, cancellationToken);
            var campaignDtos = _mapper.Map<List<CampaignDto>>(campaigns);
            return new ResponseObject<List<CampaignDto>>(campaignDtos, HttpStatusCode.OK, "success!");
        }
    }
}
