
using MediatR;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;
namespace SWD.NextIntern.Service.Services.UniversityService.Delete
{
    public class DeleteUniversityCommandHandler : IRequestHandler<DeleteUniversityCommand, ResponseObject<string>>
    {
        private readonly IUniversityRepository _universityRepository;

        public DeleteUniversityCommandHandler(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        public async Task<ResponseObject<string>> Handle(DeleteUniversityCommand request, CancellationToken cancellationToken)
        {
            var university = await _universityRepository.FindAsync(c => c.UniversityId.ToString().Equals(request.Id), cancellationToken);
            if (university == null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"University with id {request.Id} does not exist!");
            }

            _universityRepository.Remove(university);
            return await _universityRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
