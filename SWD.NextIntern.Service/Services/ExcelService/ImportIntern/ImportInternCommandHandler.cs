using MediatR;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.ExcelService.ImportIntern
{
    public class ImportInternCommandHandler : IRequestHandler<ImportInternCommand, ResponseObject<string>>
    {
        private readonly IUserRepository _userRepository;

        public ImportInternCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<ResponseObject<string>> Handle(ImportInternCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
