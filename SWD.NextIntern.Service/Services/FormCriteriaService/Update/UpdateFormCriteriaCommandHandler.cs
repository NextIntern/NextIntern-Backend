
using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Linq.Expressions;
using System.Net;

namespace SWD.NextIntern.Service.Services.FormCriteriaService.Update
{
    public class UpdateFormCriteriaCommandHandler : IRequestHandler<UpdateFormCriteriaCommand, ResponseObject<string>>
    {
        private readonly IFormCriteriaRepository _formCriteriaRepository;
        private readonly IEvaluationFormRepository _evaluationFormRepository;

        public UpdateFormCriteriaCommandHandler(IFormCriteriaRepository formCriteriaRepository, IEvaluationFormRepository evaluationFormRepository)
        {
            _formCriteriaRepository = formCriteriaRepository;
            _evaluationFormRepository = evaluationFormRepository;
        }

        public async Task<ResponseObject<string>> Handle(UpdateFormCriteriaCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<FormCriterion, bool>> queryFilter = (FormCriterion f) => f.FormCriteriaId.ToString().Equals(request.Id) && f.DeletedDate == null;

            Expression<Func<EvaluationForm, bool>> queryFilter1 = !request.EvaluationFormId.IsNullOrEmpty() ? (EvaluationForm e) => e.EvaluationFormId.ToString().Equals(request.EvaluationFormId) && e.DeletedDate == null : null;

            if (queryFilter is null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Form Criteria with id {request.Id}does not exist!");
            }

            if (!request.EvaluationFormId.IsNullOrEmpty() && queryFilter is null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Evaluation Form with id {request.EvaluationFormId}does not exist!");
            }

            var form = await _formCriteriaRepository.FindAsync(queryFilter, cancellationToken);
            var e_form = !request.EvaluationFormId.IsNullOrEmpty() ? await _evaluationFormRepository.FindAsync(queryFilter1, cancellationToken) : null;

            if (form is null) return new ResponseObject<string>(HttpStatusCode.NotFound, $"Form Criteria with id {request.Id} does not exist!");
            if (!request.EvaluationFormId.IsNullOrEmpty() && e_form is null) return new ResponseObject<string>(HttpStatusCode.NotFound, $"Evaluation Form with id {request.EvaluationFormId} does not exist!");

            form.Name = request.FormCriteriaName ?? form.Name;
            form.Guide = request.Guide ?? form.Guide;
            form.MinScore = request.MinScore;
            form.MaxScore = request.MaxScore;

            if (Guid.TryParse(request.EvaluationFormId, out Guid evaluationFormId))
            {
                form.EvaluationFormId = evaluationFormId;
            }
            else
            {
                form.EvaluationFormId = form.EvaluationFormId;
            }

            _formCriteriaRepository.Update(form);

            return await _formCriteriaRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
