﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;

namespace SWD.NextIntern.Service.Services.CampaignService.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, List<CampaignDto>>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(ICampaignRepository campaignRepository, IMapper mapper)
        {
            _campaignRepository = campaignRepository;
            _mapper = mapper;
        }

        public async Task<List<CampaignDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<Campaign> query) =>
            {
                return query.Include(x => x.University);
            };

            var campaigns = await _campaignRepository.FindAllAsync(queryOptions, cancellationToken);
            var campaignDtos = _mapper.Map<List<CampaignDto>>(campaigns);
            return campaignDtos;
        }
    }
}
