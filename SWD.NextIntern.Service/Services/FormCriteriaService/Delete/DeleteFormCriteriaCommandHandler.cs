
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;
namespace SWD.NextIntern.Service.Services.FormCriteriaService.Delete
{
    public class DeleteFormCriteriaCommandHandler : IRequestHandler<DeleteFormCriteriaCommand, ResponseObject<string>>
    {
        private readonly IFormCriteriaRepository _formCriteriaRepository;
        private readonly IInternEvaluationCriteriaRepository _internEvaluationCriteriaRepository;
        private readonly AppDbContext _dbContext;

        public DeleteFormCriteriaCommandHandler(IFormCriteriaRepository formCriteriaRepository, AppDbContext dbContext, IInternEvaluationCriteriaRepository internEvaluationCriteriaRepository)
        {
            _formCriteriaRepository = formCriteriaRepository;
            _dbContext = dbContext;
            _internEvaluationCriteriaRepository = internEvaluationCriteriaRepository;
        }

        public async Task<ResponseObject<string>> Handle(DeleteFormCriteriaCommand request, CancellationToken cancellationToken)
        {
            var form = await _formCriteriaRepository.FindAsync(c => c.FormCriteriaId.ToString().Equals(request.Id), cancellationToken);
            if (form == null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Form Criteria with id {request.Id}does not exist!");
            }

            var criteriaList = _dbContext.InternEvaluationCriteria
               .Include(s => s.InternEvaluation)
               .Where(s => s.DeletedDate == null && s.InternEvaluationId.ToString().Equals(request.Id))
               .ToList();

            foreach (var item in criteriaList)
            {
                item.DeletedDate = DateTime.Now;
                await _internEvaluationCriteriaRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }

            form.DeletedDate = DateTime.Now;

            return await _formCriteriaRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
