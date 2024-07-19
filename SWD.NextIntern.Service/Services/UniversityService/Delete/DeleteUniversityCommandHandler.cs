
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignEvaluationService.Delete;
using SWD.NextIntern.Service.Services.CampaignQuestionService.Delete;
using SWD.NextIntern.Service.Services.CampaignService.Delete;
using SWD.NextIntern.Service.Services.EvaluationFormService.Delete;
using System.Net;
namespace SWD.NextIntern.Service.Services.UniversityService.Delete
{
    public class DeleteUniversityCommandHandler : IRequestHandler<DeleteUniversityCommand, ResponseObject<string>>
    {
        private readonly IMediator _mediator;
        private readonly IUniversityRepository _universityRepository;
        private readonly ICampaignRepository _campaignRepository;
        private readonly IEvaluationFormRepository _evaluationFormRepository;
        private readonly AppDbContext _dbContext;
        private readonly DeleteCampaignCommandHandler _deleteCampaignCommandHandler;
        private readonly DeleteEvaluationFormCommandHandler _deleteEvaluationFormCommandHandler;

        public DeleteUniversityCommandHandler(IUniversityRepository universityRepository, AppDbContext dbContext, DeleteCampaignCommandHandler deleteCampaignCommandHandler, ICampaignRepository campaignRepository, DeleteEvaluationFormCommandHandler deleteEvaluationFormCommandHandler, IEvaluationFormRepository evaluationFormRepository, IMediator mediator)
        {
            _universityRepository = universityRepository;
            _dbContext = dbContext;
            _deleteCampaignCommandHandler = deleteCampaignCommandHandler;
            _campaignRepository = campaignRepository;
            _deleteEvaluationFormCommandHandler = deleteEvaluationFormCommandHandler;
            _evaluationFormRepository = evaluationFormRepository;
            _mediator = mediator;
        }

        public async Task<ResponseObject<string>> Handle(DeleteUniversityCommand request, CancellationToken cancellationToken)
        {
            var university = await _universityRepository.FindAsync(c => c.UniversityId.ToString().Equals(request.Id), cancellationToken);
            if (university == null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"University with id {request.Id} does not exist!");
            }

            var campaignList = _dbContext.Campaigns
                .Include(s => s.University)
                .Where(s => s.DeletedDate == null && s.UniversityId.ToString().Equals(request.Id))
                .ToList();

            var formList = _dbContext.EvaluationForms
                .Include(s => s.University)
                .Where(s => s.DeletedDate == null && s.UniversityId.ToString().Equals(request.Id))
                .ToList();

            foreach (var item in campaignList)
            {
                await _mediator.Send(new DeleteCampaignCommand(item.CampaignId.ToString()), cancellationToken);
            }

            foreach (var item in formList)
            {
                await _mediator.Send(new DeleteEvaluationFormCommand(item.EvaluationFormId.ToString()), cancellationToken);
            }

            university.DeletedDate = DateTime.Now;

            return await _universityRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
