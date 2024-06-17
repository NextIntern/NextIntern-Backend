using MediatR;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;

namespace SWD.NextIntern.Service.Services.CampaignEvaluationService.Delete
{
    public class DeleteCampaignEvaluationCommandHandler : IRequestHandler<DeleteCampaignEvaluationCommand, ResponseObject<string>>
    {
        private readonly ICampaignEvaluationRepository _campaignEvaluationRepository;

        public DeleteCampaignEvaluationCommandHandler(ICampaignEvaluationRepository campaignEvaluationRepository)
        {
            _campaignEvaluationRepository = campaignEvaluationRepository;
        }

        public async Task<ResponseObject<string>> Handle(DeleteCampaignEvaluationCommand request, CancellationToken cancellationToken)
        {
            var campaignEvaluation = await _campaignEvaluationRepository.FindAsync(ce => ce.CampaignEvaluationId.ToString().Equals(request.Id) && ce.DeletedDate == null, cancellationToken);

            if (campaignEvaluation == null)
            {
                return new ResponseObject<string>(System.Net.HttpStatusCode.NotFound, $"Campaign Evaluation with id {request.Id} does not exist!");
            }

            _campaignEvaluationRepository.Remove(campaignEvaluation);

            return await _campaignEvaluationRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
