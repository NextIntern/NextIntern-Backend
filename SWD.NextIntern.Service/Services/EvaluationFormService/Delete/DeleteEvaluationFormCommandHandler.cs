
using MediatR;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;
namespace SWD.NextIntern.Service.Services.EvaluationFormService.Delete
{
    public class DeleteEvaluationFormCommandHandler : IRequestHandler<DeleteEvaluationFormCommand, ResponseObject<string>>
    {
        private readonly IEvaluationFormRepository _evaluationFormRepository;

        public DeleteEvaluationFormCommandHandler(IEvaluationFormRepository evaluationFormRepository)
        {
            _evaluationFormRepository = evaluationFormRepository;
        }

        public async Task<ResponseObject<string>> Handle(DeleteEvaluationFormCommand request, CancellationToken cancellationToken)
        {
            var form = await _evaluationFormRepository.FindAsync(c => c.EvaluationFormId.ToString().Equals(request.Id), cancellationToken);
            if (form == null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Evaluation Form with id {request.Id} does not exist!");
            }

            _evaluationFormRepository.Remove(form);
            return await _evaluationFormRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
