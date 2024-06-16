
using MediatR;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;
namespace SWD.NextIntern.Service.Services.InternService.Delete
{
    public class DeleteInternCommandHandler : IRequestHandler<DeleteInternCommand, ResponseObject<string>>
    {
        private readonly IUserRepository _userRepository;

        public DeleteInternCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResponseObject<string>> Handle(DeleteInternCommand request, CancellationToken cancellationToken)
        {
            var intern = await _userRepository.FindAsync(c => c.UserId.ToString().Equals(request.Id), cancellationToken);
            if (intern == null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Evaluation Form with id {request.Id} does not exist!");
            }

            _userRepository.Remove(intern);
            return await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
