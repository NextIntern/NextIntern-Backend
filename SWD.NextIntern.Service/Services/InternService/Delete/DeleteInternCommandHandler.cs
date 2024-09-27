
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignQuestionResponseService.Delete;
using SWD.NextIntern.Service.Services.CampaignService.Delete;
using SWD.NextIntern.Service.Services.EvaluationFormService.Delete;
using SWD.NextIntern.Service.Services.InternEvaluationService.Delete;
using System.Net;
namespace SWD.NextIntern.Service.Services.InternService.Delete
{
    public class DeleteInternCommandHandler : IRequestHandler<DeleteInternCommand, ResponseObject<string>>
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;
        private readonly IInternEvaluationRepository _internEvaluationRepository;
        private readonly ICampaignQuestionResponseRepository _campaignQuestionResponseRepository;
        private readonly AppDbContext _dbContext;
        private readonly DeleteInternEvaluationCommandHandler _deleteInternEvaluationCommandHandler;
        private readonly DeleteCampaignQuestionResponseCommandHandler _deleteCampaignQuestionResponseCommandHandler;

        public DeleteInternCommandHandler(IUserRepository userRepository, AppDbContext dbContext, IInternEvaluationRepository internEvaluationRepository, DeleteInternEvaluationCommandHandler deleteInternEvaluationCommandHandler, DeleteCampaignQuestionResponseCommandHandler deleteCampaignQuestionResponseCommandHandler, ICampaignQuestionResponseRepository campaignQuestionResponseRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _dbContext = dbContext;
            _internEvaluationRepository = internEvaluationRepository;
            _deleteInternEvaluationCommandHandler = deleteInternEvaluationCommandHandler;
            _deleteCampaignQuestionResponseCommandHandler = deleteCampaignQuestionResponseCommandHandler;
            _campaignQuestionResponseRepository = campaignQuestionResponseRepository;
            _mediator = mediator;
        }

        public async Task<ResponseObject<string>> Handle(DeleteInternCommand request, CancellationToken cancellationToken)
        {
            var intern = await _userRepository.FindAsync(c => c.UserId.ToString().Equals(request.Id), cancellationToken);
            if (intern == null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Evaluation Form with id {request.Id} does not exist!");
            }

            var internEvaluationList = _dbContext.InternEvaluations
                .Include(s => s.Intern)
                .Where(s => s.DeletedDate == null && s.InternId.ToString().Equals(request.Id))
                .ToList();

            var responseList = _dbContext.CampaignQuestionResponses
                .Include(s => s.Intern)
                .Where(s => s.DeletedDate == null && s.InternId.ToString().Equals(request.Id))
                .ToList();

            foreach (var item in internEvaluationList)
            {
                await _mediator.Send(new DeleteInternEvaluationCommand(item.InternEvaluationId.ToString()), cancellationToken);
            }

            foreach (var item in responseList)
            {
                await _mediator.Send(new DeleteCampaignQuestionResponseCommand(item.CampaignQuestionResponseId.ToString()), cancellationToken);
            }

            intern.DeletedDate = DateTime.Now;

            return await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
