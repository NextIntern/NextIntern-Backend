using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignQuestionService.Delete;
using SWD.NextIntern.Service.Services.InternEvaluationService.Delete;
using System.Net;

namespace SWD.NextIntern.Service.Services.CampaignEvaluationService.Delete
{
    public class DeleteCampaignEvaluationCommandHandler : IRequestHandler<DeleteCampaignEvaluationCommand, ResponseObject<string>>
    {
        private readonly IMediator _mediator;
        private readonly ICampaignEvaluationRepository _campaignEvaluationRepository;
        private readonly IInternEvaluationRepository _internEvaluationRepository;
        private readonly AppDbContext _dbContext;
        private readonly DeleteInternEvaluationCommandHandler _deleteInternEvaluationCommandHandler;

        public DeleteCampaignEvaluationCommandHandler(ICampaignEvaluationRepository campaignEvaluationRepository, AppDbContext dbContext, IInternEvaluationRepository internEvaluationRepository, DeleteInternEvaluationCommandHandler deleteInternEvaluationCommandHandler, IMediator mediator)
        {
            _campaignEvaluationRepository = campaignEvaluationRepository;
            _dbContext = dbContext;
            _internEvaluationRepository = internEvaluationRepository;
            _deleteInternEvaluationCommandHandler = deleteInternEvaluationCommandHandler;
            _mediator = mediator;
        }

        public async Task<ResponseObject<string>> Handle(DeleteCampaignEvaluationCommand request, CancellationToken cancellationToken)
        {
            var campaignEvaluation = await _campaignEvaluationRepository.FindAsync(ce => ce.CampaignEvaluationId.ToString().Equals(request.Id) && ce.DeletedDate == null, cancellationToken);

            if (campaignEvaluation == null)
            {
                return new ResponseObject<string>(System.Net.HttpStatusCode.NotFound, $"Campaign Evaluation with id {request.Id} does not exist!");
            }

            var evaluationList = _dbContext.InternEvaluations
            .Include(s => s.CampaignEvaluation)
            .Where(s => s.DeletedDate == null && s.CampaignEvaluationId.ToString().Equals(request.Id))
            .ToList();

            foreach (var item in evaluationList)
            {
                await _mediator.Send(new DeleteInternEvaluationCommand(item.InternEvaluationId.ToString()), cancellationToken);
            }

            campaignEvaluation.DeletedDate = DateTime.Now;

            return await _campaignEvaluationRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
