using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Linq.Expressions;
using System.Net;


namespace SWD.NextIntern.Service.Services.EvaluationFormService.Create
{
    public class CreateEvaluationFormCommandHandler : IRequestHandler<CreateEvaluationFormCommand, ResponseObject<string>>
    {
        private readonly IEvaluationFormRepository _evaluationFormRepository;
        private readonly IUniversityRepository _universityRepository;

        public CreateEvaluationFormCommandHandler(IEvaluationFormRepository evaluationFormRepository, IUniversityRepository universityRepository)
        {
            _evaluationFormRepository = evaluationFormRepository;
            _universityRepository = universityRepository;
        }

        public async Task<ResponseObject<string>> Handle(CreateEvaluationFormCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<University, bool>> queryFilter = (University u) => u.Id.ToString().Equals(request.UniversityId) && u.DeletedDate == null;

            if (queryFilter is null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"University with id {request.UniversityId} does not exist!");
            }

            var form = new EvaluationForm
            {
                UniversityId = request.UniversityId,
                IsActive = request.IsActive,
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.Now,
                DeletedDate = null
            };
            _evaluationFormRepository.Add(form);

            return await _evaluationFormRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.Created, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
