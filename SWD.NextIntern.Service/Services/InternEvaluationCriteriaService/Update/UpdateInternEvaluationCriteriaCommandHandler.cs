
using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Linq.Expressions;
using System.Net;

namespace SWD.NextIntern.Service.Services.InternEvaluationCriteriaService.Update
{
    public class UpdateInternEvaluationCriteriaCommandHandler : IRequestHandler<UpdateInternEvaluationCriteriaCommand, ResponseObject<string>>
    {
        private readonly IInternEvaluationCriteriaRepository _internEvaluationCriteriaRepository;
        private readonly IInternEvaluationRepository _internEvaluationRepository;
        private readonly IFormCriteriaRepository _formCriteriaRepository;

        public UpdateInternEvaluationCriteriaCommandHandler(IInternEvaluationCriteriaRepository internEvaluationCriteriaRepository, IInternEvaluationRepository internEvaluationRepository, IFormCriteriaRepository formCriteriaRepository)
        {
            _internEvaluationCriteriaRepository = internEvaluationCriteriaRepository;
            _internEvaluationRepository = internEvaluationRepository;
            _formCriteriaRepository = formCriteriaRepository;
        }

        public async Task<ResponseObject<string>> Handle(UpdateInternEvaluationCriteriaCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<InternEvaluationCriterion, bool>> queryFilter = (InternEvaluationCriterion u) => u.InternEvaluationCriteriaId.ToString().Equals(request.InternEvaluationCriteriaId) && u.DeletedDate == null;
            Expression<Func<InternEvaluation, bool>> queryFilter1 = (!request.InternEvaluationId.IsNullOrEmpty()) ? (InternEvaluation c) => c.InternEvaluationId.ToString().Equals(request.InternEvaluationId) && c.DeletedDate == null : null;
            Expression<Func<FormCriterion, bool>> queryFilter2 = (!request.FromCriteriaId.IsNullOrEmpty()) ? (FormCriterion r) => r.FormCriteriaId.Equals(request.FromCriteriaId) && r.DeletedDate == null : null;
          
            if (queryFilter is null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"InternEvaluationCriteria with id {request.InternEvaluationCriteriaId}does not exist!");
            }         

            if (!request.InternEvaluationId.IsNullOrEmpty() && queryFilter1 is null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"InternEvaluation with id {request.InternEvaluationId}does not exist!");
            }                

            if (!request.FromCriteriaId.IsNullOrEmpty() && queryFilter2 is null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"FromCriteria with name {request.FromCriteriaId}does not exist!");
            }

            var ivaCriteria = await _internEvaluationCriteriaRepository.FindAsync(queryFilter, cancellationToken);
            var iva = !request.InternEvaluationId.IsNullOrEmpty() ? await _internEvaluationRepository.FindAsync(queryFilter1, cancellationToken) : null;
            var formCriteria = !request.FromCriteriaId.IsNullOrEmpty() ? await _formCriteriaRepository.FindAsync(queryFilter2, cancellationToken) : null;

            if (ivaCriteria is null) return new ResponseObject<string>(HttpStatusCode.NotFound, $"InternEvaluationCriteria with id {request.InternEvaluationCriteriaId}does not exist!");
            if (!request.InternEvaluationId.IsNullOrEmpty() && iva is null) return new ResponseObject<string>(HttpStatusCode.NotFound, $"InternEvaluation with id {request.InternEvaluationId}does not exist!");
            if (!request.FromCriteriaId.IsNullOrEmpty() && formCriteria is null) return new ResponseObject<string>(HttpStatusCode.NotFound, $"FromCriteria with name {request.FromCriteriaId}does not exist!");

            ivaCriteria.Score = request.Score;
                

            if (request.InternEvaluationId != null && Guid.TryParse(request.InternEvaluationId, out Guid internEvaluationId))
            {
                ivaCriteria.InternEvaluationId = internEvaluationId;
            }
            else
            {
                ivaCriteria.InternEvaluationId = ivaCriteria.InternEvaluationId;
            }

            if (request.FromCriteriaId != null && Guid.TryParse(request.FromCriteriaId, out Guid fromCriteriaId))
            {
                ivaCriteria.FormCriteriaId = fromCriteriaId;
            }
            else
            {
                ivaCriteria.FormCriteriaId = ivaCriteria.FormCriteriaId;
            }

            _internEvaluationCriteriaRepository.Update(ivaCriteria);

            return await _internEvaluationCriteriaRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
