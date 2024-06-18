
using MediatR;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;
namespace SWD.NextIntern.Service.Services.FormCriteriaService.Delete
{
    public class DeleteFormCriteriaCommandHandler : IRequestHandler<DeleteFormCriteriaCommand, ResponseObject<string>>
    {
        private readonly IFormCriteriaRepository _formCriteriaRepository;

        public DeleteFormCriteriaCommandHandler(IFormCriteriaRepository formCriteriaRepository)
        {
            _formCriteriaRepository = formCriteriaRepository;
        }

        public async Task<ResponseObject<string>> Handle(DeleteFormCriteriaCommand request, CancellationToken cancellationToken)
        {
            var form = await _formCriteriaRepository.FindAsync(c => c.FormCriteriaId.ToString().Equals(request.Id), cancellationToken);
            if (form == null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Form Criteria with id {request.Id}does not exist!");
            }

            _formCriteriaRepository.Remove(form);
            return await _formCriteriaRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
