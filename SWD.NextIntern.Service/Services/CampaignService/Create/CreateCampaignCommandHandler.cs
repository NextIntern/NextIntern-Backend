using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;

namespace SWD.NextIntern.Service.Services.CampaignService.Create
{
    public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, ResponseObject<string>>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IUniversityRepository _universityRepository;
        public CreateCampaignCommandHandler(ICampaignRepository campaignRepository, IUniversityRepository universityRepository)
        {
            _campaignRepository = campaignRepository;
            _universityRepository = universityRepository;
        }

        public async Task<ResponseObject<string>> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            var existCampaign = await _campaignRepository.FindAsync(c => c.CampaignName.Equals(request.CampaignName) && c.DeletedDate == null, cancellationToken);
            if (existCampaign != null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Campaign with name {request.CampaignName} is exist!");
            }

            var university = await _universityRepository.FindAsync(u => u.UniversityId.ToString().Equals(request.UniversityId) && u.DeletedDate == null, cancellationToken);

            if (university is null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"University with id {request.UniversityId} does not exist!");
            }

            var campaign = new Campaign
            {
                CampaignName = request.CampaignName,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                UniversityId = Guid.Parse(request.UniversityId),
            };

            _campaignRepository.Add(campaign);

            return await _campaignRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.Created, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
