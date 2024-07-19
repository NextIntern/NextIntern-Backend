using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignEvaluationService.Delete;
using SWD.NextIntern.Service.Services.CampaignQuestionService.Delete;
using SWD.NextIntern.Service.Services.InternEvaluationService.Delete;
using System.Net;
namespace SWD.NextIntern.Service.Services.CampaignService.Delete
{
    public class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommand, ResponseObject<string>>
    {
        private readonly IMediator _mediator;
        private readonly ICampaignRepository _campaignRepository;
        private readonly ICampaignEvaluationRepository _campaignEvaluationRepository;
        private readonly ICampaignQuestionRepository _campaignQuestionRepository;
        private readonly IInternEvaluationRepository _internEvaluationRepository;
        private readonly AppDbContext _dbContext;
        private readonly DeleteCampaignQuestionCommandHandler _deleteCampaignQuestionCommandHandler;
        private readonly DeleteCampaignEvaluationCommandHandler _deleteCampaignEvaluationCommandHandler;

        public DeleteCampaignCommandHandler(ICampaignRepository campaignRepository, AppDbContext dbContext, ICampaignEvaluationRepository campaignEvaluationRepository, DeleteCampaignQuestionCommandHandler deleteCampaignQuestionCommandHandler, ICampaignQuestionRepository campaignQuestionRepository, DeleteInternEvaluationCommandHandler deleteInternEvaluationCommandHandler, DeleteCampaignEvaluationCommandHandler deleteCampaignEvaluationCommandHandler, IMediator mediator)
        {
            _campaignRepository = campaignRepository;
            _dbContext = dbContext;
            _campaignEvaluationRepository = campaignEvaluationRepository;
            _deleteCampaignQuestionCommandHandler = deleteCampaignQuestionCommandHandler;
            _campaignQuestionRepository = campaignQuestionRepository;
            _deleteCampaignEvaluationCommandHandler = deleteCampaignEvaluationCommandHandler;
            _mediator = mediator;
        }

        public async Task<ResponseObject<string>> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignRepository.FindAsync(c => c.CampaignId.ToString().Equals(request.Id) && c.DeletedDate == null, cancellationToken);
            if (campaign == null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Campaign with id {request.Id} does not exist!");
            }

            var campaignList = _dbContext.CampaignEvaluations
              .Include(s => s.Campaign)
              .Where(s => s.DeletedDate == null && s.CampaignId.ToString().Equals(request.Id))
              .ToList();

            var campaignQuestion = _dbContext.CampaignQuestions
              .Include(s => s.Campaign)
              .Where(s => s.DeletedDate == null && s.CampaignId.ToString().Equals(request.Id))
              .ToList();

            foreach (var item in campaignQuestion)
            {
                await _mediator.Send(new DeleteCampaignQuestionCommand(item.CampaignQuestionId.ToString()), cancellationToken);
            }

            foreach (var item in campaignList)
            {
                await _mediator.Send(new DeleteCampaignEvaluationCommand(item.CampaignEvaluationId.ToString()), cancellationToken);
            }

            campaign.DeletedDate = DateTime.Now;

            return await _campaignRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
