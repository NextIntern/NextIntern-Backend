
using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.IRepositories;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;

namespace SWD.NextIntern.Service.InternEvaluationCriteriaService.Create
{
    public class CreateInternEvaluationCriteriaCommandHandler : IRequestHandler<CreateInternEvaluationCriteriaCommand, ResponseObject<string>>
    {
        private readonly IInternEvaluationCriteriaRepository _internEvaluationCriteriaRepository;
        private readonly IInternEvaluationRepository _internEvaluationRepository;
        private readonly IFormCriteriaRepository _formCriteriaRepository;

        public CreateInternEvaluationCriteriaCommandHandler(IInternEvaluationCriteriaRepository internEvaluationCriteriaRepository, IInternEvaluationRepository internEvaluationRepository, IFormCriteriaRepository formCriteriaRepository)
        {
            _internEvaluationCriteriaRepository = internEvaluationCriteriaRepository;
            _internEvaluationRepository = internEvaluationRepository;
            _formCriteriaRepository = formCriteriaRepository;
        }

        public async Task<ResponseObject<string>> Handle(CreateInternEvaluationCriteriaCommand request, CancellationToken cancellationToken)
        {
            var ieva = await _internEvaluationRepository.FindAsync(i => i.InternEvaluationId.ToString().Equals(request.InternEvaluationId) && i.DeletedDate == null);
            var formCriteria = await _formCriteriaRepository.FindAsync(i => i.FormCriteriaId.ToString().Equals(request.FromCriteriaId) && i.DeletedDate == null);

            if (ieva == null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"InternEvaluationId with id {request.InternEvaluationId} does not exist!");
            }

            if (formCriteria == null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"FromCriteriaId with id {request.FromCriteriaId} does not exist!");
            }

            var newIevaCriteria = new InternEvaluationCriterion
            {
                InternEvaluationId = Guid.Parse(request.InternEvaluationId),
                FormCriteriaId = Guid.Parse(request.FromCriteriaId),
                Score = request.Score
            };

            _internEvaluationCriteriaRepository.Add(newIevaCriteria);

            return await _internEvaluationCriteriaRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.Created, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
