using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Linq.Expressions;
using System.Net;

namespace SWD.NextIntern.Service.Services.FormCriteriaService.CreateList
{
    public class CreateListFormCriteriaCommandHandler : IRequestHandler<CreateListFormCriteriaCommand, ResponseObject<string>>
    {
        private readonly IFormCriteriaRepository _formCriteriaRepository;
        private readonly IEvaluationFormRepository _evaluationFormRepository;

        public CreateListFormCriteriaCommandHandler(IFormCriteriaRepository formCriteriaRepository, IEvaluationFormRepository evaluationFormRepository)
        {
            _formCriteriaRepository = formCriteriaRepository;
            _evaluationFormRepository = evaluationFormRepository;
        }

        public async Task<ResponseObject<string>> Handle(CreateListFormCriteriaCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<EvaluationForm, bool>> queryFilter = (EvaluationForm f) => f.EvaluationFormId.ToString().Equals(request.EvaluationFormId) && f.DeletedDate == null;

            var evaluationForm = await _evaluationFormRepository.FindAsync(queryFilter, cancellationToken);

            if (evaluationForm is null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Evaluation Form with id {request.EvaluationFormId} does not exist!");
            }

            foreach (var item in request.ListFormCriteria)
            {
                var form = new FormCriterion
                {
                    Name = item.FormCriteriaName,
                    Guide = item.Guide,
                    MinScore = item.MinScore,
                    MaxScore = item.MaxScore,
                    DeletedDate = null,
                    EvaluationFormId = Guid.Parse(request.EvaluationFormId),
                };

                _formCriteriaRepository.Add(form);
            }

            return await _formCriteriaRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.Created, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");

        }
    }
}
