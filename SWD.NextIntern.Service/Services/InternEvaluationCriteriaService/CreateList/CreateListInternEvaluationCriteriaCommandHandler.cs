using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;

namespace SWD.NextIntern.Service.Services.InternEvaluationCriteriaService.CreateList
{
    public class CreateListInternEvaluationCriteriaCommandHandler : IRequestHandler<CreateListInternEvaluationCriteriaCommand, ResponseObject<string>>
    {
        private readonly IInternEvaluationCriteriaRepository _internEvaluationCriteriaRepository;
        private readonly IInternEvaluationRepository _internEvaluationRepository;
        private readonly IFormCriteriaRepository _formCriteriaRepository;

        public CreateListInternEvaluationCriteriaCommandHandler(
            IInternEvaluationCriteriaRepository internEvaluationCriteriaRepository,
            IInternEvaluationRepository internEvaluationRepository,
            IFormCriteriaRepository formCriteriaRepository)
        {
            _internEvaluationCriteriaRepository = internEvaluationCriteriaRepository;
            _internEvaluationRepository = internEvaluationRepository;
            _formCriteriaRepository = formCriteriaRepository;
        }

        public async Task<ResponseObject<string>> Handle(CreateListInternEvaluationCriteriaCommand request, CancellationToken cancellationToken)
        {

            var ieva = new InternEvaluation
            {
                CampaignEvaluationId = Guid.Parse(request.CampaignEvaluationId),
                InternId = Guid.Parse(request.InternId),
            };

            _internEvaluationRepository.Add(ieva);
            var result = await _internEvaluationRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            if (result > 1) return new ResponseObject<string>(HttpStatusCode.NotFound, $"Cannot create InternEvaluation");

            foreach (var item in request.InternEvaluationCriterias)
            {
                var formCriteria = await _formCriteriaRepository.FindAsync(i =>
                    i.FormCriteriaId.ToString().Equals(item.FormCriteriaId) &&
                    i.DeletedDate == null);

                if (formCriteria == null)
                {
                    return new ResponseObject<string>(HttpStatusCode.NotFound, $"FromCriteriaId with id {item.FormCriteriaId} does not exist!");
                }

                var newIevaCriteria = new InternEvaluationCriterion
                {
                    InternEvaluationId = ieva.InternEvaluationId,
                    FormCriteriaId = Guid.Parse(item.FormCriteriaId),
                    Score = item.Score
                };

                _internEvaluationCriteriaRepository.Add(newIevaCriteria);
            }

            return await _internEvaluationCriteriaRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0
                ? new ResponseObject<string>(HttpStatusCode.Created, "Success!")
                : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
