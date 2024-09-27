
using AutoMapper;
using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Linq.Expressions;
using System.Net;

namespace SWD.NextIntern.Service.Services.EvaluationFormService.Update
{
    public class UpdateEvaluationFormCommandHandler : IRequestHandler<UpdateEvaluationFormCommand, ResponseObject<string>>
    {
        private readonly IEvaluationFormRepository _evaluationFormRepository;

        public UpdateEvaluationFormCommandHandler(IEvaluationFormRepository evaluationFormRepository)
        {
            _evaluationFormRepository = evaluationFormRepository;
        }

        public async Task<ResponseObject<string>> Handle(UpdateEvaluationFormCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<EvaluationForm, bool>> queryFilter = (EvaluationForm f) => f.EvaluationFormId.ToString().Equals(request.Id) && f.DeletedDate == null;

            if (queryFilter is null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Evaluation Form with id {request.Id} does not exist!");
            }

            Expression<Func<University, bool>> queryFilter1 = (University u) => u.Id.ToString().Equals(request.UniversityId) && u.DeletedDate == null;

            if (queryFilter1 is null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"University with id {request.UniversityId} does not exist!");
            }

            var form = await _evaluationFormRepository.FindAsync(queryFilter, cancellationToken);

            if (form is null) return new ResponseObject<string>(HttpStatusCode.NotFound, $"Evaluation Form with id {request.Id} does not exist!");

            form.IsActive = request.IsActive;
            form.CreateDate = DateTime.Now;
            form.ModifyDate = DateTime.Now;

            if (Guid.TryParse(request.UniversityId, out Guid universityId))
            {
                form.UniversityId = universityId;
            }
            else
            {
                form.UniversityId = form.UniversityId;
            }

            _evaluationFormRepository.Update(form);

            return await _evaluationFormRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
