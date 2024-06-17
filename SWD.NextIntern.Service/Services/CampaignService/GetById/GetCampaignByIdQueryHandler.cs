using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;

namespace SWD.NextIntern.Service.Services.CampaignService.GetById
{
    public class GetCampaignByIdQueryHandler : IRequestHandler<GetCampaignByIdQuery, ResponseObject<CampaignDto>>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMapper _mapper;

        public GetCampaignByIdQueryHandler(IMapper mapper, ICampaignRepository campaignRepository)
        {
            _mapper = mapper;
            _campaignRepository = campaignRepository;
        }

        public async Task<ResponseObject<CampaignDto>> Handle(GetCampaignByIdQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<Campaign> query) =>
            {
                return query.Include(x => x.University);
            };

            var campaign = await _campaignRepository.FindAsync(c => c.CampaignId.ToString().Equals(request.Id) && c.DeletedDate == null, queryOptions, cancellationToken);
            if (campaign is null)
            {
                return new ResponseObject<CampaignDto>(HttpStatusCode.NotFound, $"Campaign with id {request.Id} doest not exist!");
            }
            return new ResponseObject<CampaignDto>(_mapper.Map<CampaignDto>(campaign), HttpStatusCode.OK, "success!");
        }
    }
}
