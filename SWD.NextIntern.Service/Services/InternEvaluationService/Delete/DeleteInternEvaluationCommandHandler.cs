using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;

namespace SWD.NextIntern.Service.Services.InternEvaluationService.Delete
{
    public class DeleteInternEvaluationCommandHandler : IRequestHandler<DeleteInternEvaluationCommand, ResponseObject<string>>
    {
        private readonly IInternEvaluationRepository _internEvaluationRepository;
        private readonly IInternEvaluationCriteriaRepository _internEvaluationCriteriaRepository;
        private readonly AppDbContext _dbContext;

        public DeleteInternEvaluationCommandHandler(IInternEvaluationRepository internEvaluationRepository, IInternEvaluationCriteriaRepository internEvaluationCriteriaRepository, AppDbContext dbContext)
        {
            _internEvaluationRepository = internEvaluationRepository;
            _internEvaluationCriteriaRepository = internEvaluationCriteriaRepository;
            _dbContext = dbContext;
        }

        public async Task<ResponseObject<string>> Handle(DeleteInternEvaluationCommand request, CancellationToken cancellationToken)
        {
            var internEvaluation = await _internEvaluationRepository.FindAsync(ie => ie.InternEvaluationId.ToString().Equals(request.Id) && ie.DeletedDate == null, cancellationToken);

            if (internEvaluation == null)
            {
                return new ResponseObject<string>(System.Net.HttpStatusCode.NotFound, $"Intern Evaluation with id {request.Id} does not exist!");
            }

            var criteriaList = _dbContext.InternEvaluationCriteria
               .Include(s => s.InternEvaluation)
               .Where(s => s.DeletedDate == null && s.InternEvaluationId.ToString().Equals(request.Id))
               .ToList();

            //_dbContext.InternEvaluationCriteria.RemoveRange(criteriaList);
            //await _dbContext.SaveChangesAsync();

            foreach (var item in criteriaList)
            {
                item.DeletedDate = DateTime.Now;
                await _internEvaluationCriteriaRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }

            internEvaluation.DeletedDate = DateTime.Now;

            return await _internEvaluationRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
