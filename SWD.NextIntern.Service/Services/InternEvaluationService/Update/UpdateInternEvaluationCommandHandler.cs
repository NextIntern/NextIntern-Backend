using MediatR;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;

namespace SWD.NextIntern.Service.Services.InternEvaluationService.Update
{
    public class UpdateInternEvaluationCommandHandler : IRequestHandler<UpdateInternEvaluationCommand, ResponseObject<string>>
    {
        private readonly IInternEvaluationRepository _internEvaluationRepository;

        public UpdateInternEvaluationCommandHandler(IInternEvaluationRepository internEvaluationRepository)
        {
            _internEvaluationRepository = internEvaluationRepository;
        }

        public async Task<ResponseObject<string>> Handle(UpdateInternEvaluationCommand request, CancellationToken cancellationToken)
        {
            var internEvaluation = await _internEvaluationRepository.FindAsync(ie => ie.InternEvaluationId.ToString().Equals(request.Id) && ie.DeletedDate == null, cancellationToken);
            if (internEvaluation == null)
            {
                return new ResponseObject<string>(System.Net.HttpStatusCode.NotFound, $"Intern Evaluation with id {request.Id} does not exist!");
            }

            internEvaluation.CampaignEvaluationId = Guid.Parse(request.CampaignEvaluationId);
            internEvaluation.InternId = Guid.Parse(request.InternId);
            internEvaluation.Feedback = request.Feedback;

            _internEvaluationRepository.Update(internEvaluation);
            return await _internEvaluationRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
