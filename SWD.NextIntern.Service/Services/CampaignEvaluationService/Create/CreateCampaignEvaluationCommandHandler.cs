using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignEvaluationService.Create
{
    public class CreateCampaignEvaluationCommandHandler : IRequestHandler<CreateCampaignEvaluationCommand, ResponseObject<string>>
    {
        private readonly ICampaignEvaluationRepository _campaignEvaluationRepository;
        private readonly ICampaignRepository _campaignRepository;

        public CreateCampaignEvaluationCommandHandler(ICampaignEvaluationRepository campaignEvaluationRepository, ICampaignRepository campaignRepository)
        {
            _campaignEvaluationRepository = campaignEvaluationRepository;
            _campaignRepository = campaignRepository;
        }

        public async Task<ResponseObject<string>> Handle(CreateCampaignEvaluationCommand request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignRepository.FindAsync(c => c.CampaignId.ToString().Equals(request.CampaignId) && c.DeletedDate == null, cancellationToken);

            if (campaign is null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Campaign with id {request.CampaignId} does not exist!");
            }

            var campaignEvaluation = new CampaignEvaluation
            {
                CampaignId = Guid.Parse(request.CampaignId),
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };

            _campaignEvaluationRepository.Add(campaignEvaluation);

            return await _campaignEvaluationRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.Created, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
