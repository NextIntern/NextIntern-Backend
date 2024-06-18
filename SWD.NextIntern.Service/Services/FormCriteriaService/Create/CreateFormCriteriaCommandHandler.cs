using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Linq.Expressions;
using System.Net;


namespace SWD.NextIntern.Service.Services.FormCriteriaService.Create
{
    public class CreateEvaluationFormCommandHandler : IRequestHandler<CreateFormCriteriaCommand, ResponseObject<string>>
    {
        private readonly IEvaluationFormRepository _evaluationFormRepository;
        private readonly IFormCriteriaRepository _formCriteriaRepository;

        public CreateEvaluationFormCommandHandler(IEvaluationFormRepository evaluationFormRepository, IFormCriteriaRepository formCriteriaRepository)
        {
            _evaluationFormRepository = evaluationFormRepository;
            _formCriteriaRepository = formCriteriaRepository;
        }

        public async Task<ResponseObject<string>> Handle(CreateFormCriteriaCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<EvaluationForm, bool>> queryFilter = (EvaluationForm f) => f.EvaluationFormId.ToString().Equals(request.EvaluationFormId) && f.DeletedDate == null;

            if (queryFilter is null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Evaluation Form with id {request.EvaluationFormId}does not exist!");
            }

            var form = new FormCriterion
            {
                Name = request.FormCriteriaName,
                Guide = request.Guide,
                MinScore = request.MinScore,
                MaxScore = request.MaxScore,
                DeletedDate = null,
                EvaluationFormId = request.EvaluationFormId,
            };
            _formCriteriaRepository.Add(form);

            return await _formCriteriaRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.Created, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
