using AutoMapper;
using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Linq.Expressions;
using System.Net;

namespace SWD.NextIntern.Service.Services.CampaignService.Update
{
    public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand, ResponseObject<string>>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IUniversityRepository _universityRepository;

        public UpdateCampaignCommandHandler(ICampaignRepository campaignRepository, IUniversityRepository universityRepository)
        {
            _campaignRepository = campaignRepository;
            _universityRepository = universityRepository;
        }

        public async Task<ResponseObject<string>> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<Campaign, bool>> queryFilter = (Campaign c) => c.CampaignId.ToString().Equals(request.Id) && c.DeletedDate == null;

            //check university is not null
            var university = await _universityRepository.FindAsync(u => u.UniversityId.ToString().Equals(request.UniversityId), cancellationToken);

            if (university is null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"University with id {request.UniversityId} doest not exist!");
            }

            var campaign = await _campaignRepository.FindAsync(queryFilter, cancellationToken);

            if (campaign is null) return new ResponseObject<string>(HttpStatusCode.NotFound, $"Campaign with id {request.Id} does not exist!");

            campaign.StartDate = request.StartDate ?? campaign.StartDate;
            campaign.EndDate = request.EndDate ?? campaign.EndDate;
            campaign.CampaignName = request.CampaignName ?? campaign.CampaignName;
            if (Guid.TryParse(request.UniversityId, out Guid universityId))
            {
                campaign.UniversityId = universityId;
            }
            else
            {
                campaign.UniversityId = campaign.UniversityId; // No change if parsing fails
            }

            _campaignRepository.Update(campaign);

            return await _campaignRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
