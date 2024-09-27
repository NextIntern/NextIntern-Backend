
using MediatR;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;
namespace SWD.NextIntern.Service.Services.InternEvaluationCriteriaService.Delete
{
    public class DeleteInternEvaluationCriteriaCommandHandler : IRequestHandler<DeleteInternEvaluationCriteriaCommand, ResponseObject<string>>
    {
        private readonly IInternEvaluationCriteriaRepository _internEvaluationCriteriaRepository;

        public DeleteInternEvaluationCriteriaCommandHandler(IInternEvaluationCriteriaRepository internEvaluationCriteriaRepository)
        {
            _internEvaluationCriteriaRepository = internEvaluationCriteriaRepository;
        }

        public async Task<ResponseObject<string>> Handle(DeleteInternEvaluationCriteriaCommand request, CancellationToken cancellationToken)
        {
            var iaCriteria = await _internEvaluationCriteriaRepository.FindAsync(c => c.InternEvaluationCriteriaId.ToString().Equals(request.Id), cancellationToken);
            if (iaCriteria == null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"InternEvaluationCriteriaId with id {request.Id} does not exist!");
            }

            iaCriteria.DeletedDate = DateTime.Now;

            return await _internEvaluationCriteriaRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
