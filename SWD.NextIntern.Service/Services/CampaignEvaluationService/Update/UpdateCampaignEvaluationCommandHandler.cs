using MediatR;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;

namespace SWD.NextIntern.Service.Services.CampaignEvaluationService.Update
{
    public class UpdateCampaignEvaluationCommandHandler : IRequestHandler<UpdateCampaignEvaluationCommand, ResponseObject<string>>
    {
        private readonly ICampaignEvaluationRepository _campaignEvaluationRepository;
        private readonly ICampaignRepository _campaignRepository;

        public UpdateCampaignEvaluationCommandHandler(ICampaignEvaluationRepository campaignEvaluationRepository, ICampaignRepository campaignRepository)
        {
            _campaignEvaluationRepository = campaignEvaluationRepository;
            _campaignRepository = campaignRepository;
        }

        public async Task<ResponseObject<string>> Handle(UpdateCampaignEvaluationCommand request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignRepository.FindAsync(c => c.CampaignId.ToString().Equals(request.CampaignId) && c.DeletedDate == null, cancellationToken);

            if (campaign == null)
            {
                return new ResponseObject<string>(System.Net.HttpStatusCode.NotFound, $"Campaign with id {request.CampaignId} does not exist!");
            }

            var campaignEvaluation = await _campaignEvaluationRepository.FindAsync(ce => ce.CampaignEvaluationId.ToString().Equals(request.Id) && ce.DeletedDate == null, cancellationToken);

            if (campaignEvaluation == null)
            {
                return new ResponseObject<string>(System.Net.HttpStatusCode.NotFound, $"Campaign Evaluation with id {request.Id} does not exist!");
            }

            campaignEvaluation.StartDate = request.StartDate;
            campaignEvaluation.EndDate = request.EndDate;
            campaignEvaluation.CampaignId = Guid.Parse(request.CampaignId);

            _campaignEvaluationRepository.Update(campaignEvaluation);

            return await _campaignEvaluationRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.Created, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
