
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignQuestionService.Delete;
using SWD.NextIntern.Service.Services.FormCriteriaService.Delete;
using System.Net;
namespace SWD.NextIntern.Service.Services.EvaluationFormService.Delete
{
    public class DeleteEvaluationFormCommandHandler : IRequestHandler<DeleteEvaluationFormCommand, ResponseObject<string>>
    {
        private readonly IMediator _mediator;
        private readonly IEvaluationFormRepository _evaluationFormRepository;
        private readonly IFormCriteriaRepository _formCriteriaRepository;
        private readonly AppDbContext _dbContext;
        private readonly DeleteFormCriteriaCommandHandler _deleteFormCriteriaCommandHandler;

        public DeleteEvaluationFormCommandHandler(IEvaluationFormRepository evaluationFormRepository, AppDbContext dbContext, IFormCriteriaRepository formCriteriaRepository, DeleteFormCriteriaCommandHandler deleteFormCriteriaCommandHandler, IMediator mediator)
        {
            _evaluationFormRepository = evaluationFormRepository;
            _dbContext = dbContext;
            _formCriteriaRepository = formCriteriaRepository;
            _deleteFormCriteriaCommandHandler = deleteFormCriteriaCommandHandler;
            _mediator = mediator;
        }

        public async Task<ResponseObject<string>> Handle(DeleteEvaluationFormCommand request, CancellationToken cancellationToken)
        {
            var form = await _evaluationFormRepository.FindAsync(c => c.EvaluationFormId.ToString().Equals(request.Id), cancellationToken);
            if (form == null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Evaluation Form with id {request.Id} does not exist!");
            }

            var formList = _dbContext.FormCriteria
              .Include(s => s.EvaluationForm)
              .Where(s => s.DeletedDate == null && s.EvaluationFormId.ToString().Equals(request.Id))
              .ToList();

            foreach (var item in formList)
            {
                await _mediator.Send(new DeleteFormCriteriaCommand(item.FormCriteriaId.ToString()), cancellationToken);
            }

            form.DeletedDate = DateTime.Now;

            return await _evaluationFormRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
