using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using System.Linq.Expressions;

namespace SWD.NextIntern.Service.Services.CampaignService.GetById
{
    public class GetCampaignByIdQueryHandler : IRequestHandler<GetCampaignByIdQuery, CampaignDto?>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMapper _mapper;

        public GetCampaignByIdQueryHandler(IMapper mapper, ICampaignRepository campaignRepository)
        {
            _mapper = mapper;
            _campaignRepository = campaignRepository;
        }

        public async Task<CampaignDto?> Handle(GetCampaignByIdQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<Campaign> query) =>
            {
                return query.Include(x => x.University);
            };

            Expression<Func<Campaign, bool>> queryFilter = (Campaign c) => c.CampaignId.ToString().Equals(request.Id) && c.DeletedDate == null;

            var campaign = await _campaignRepository.FindAsync(queryFilter, queryOptions, cancellationToken);
            if (campaign is null)
            {
                return null;
            }
            return _mapper.Map<CampaignDto>(campaign);
        }
    }
}
