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

        public CreateCampaignCommandHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<ResponseObject<string>> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            //cần repo university để tham chieu

            var campaign = new Campaign
            {
                CampaignName = request.CampaignName,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                UniversityId = request.UniversityId,
            };

            _campaignRepository.Add(campaign);

            return await _campaignRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.Created,"success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "failed!");
        }
    }
}
