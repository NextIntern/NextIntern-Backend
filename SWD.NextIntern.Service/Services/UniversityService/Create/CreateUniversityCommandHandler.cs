using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;


namespace SWD.NextIntern.Service.Services.UniversityService.Create
{
    public class CreateUniversityCommandHandler : IRequestHandler<CreateUniversityCommand, string>
    {
        private readonly IUniversityRepository _universityRepository;

        public CreateUniversityCommandHandler(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        public async Task<string> Handle(CreateUniversityCommand request, CancellationToken cancellationToken)
        {
            var university = new University
            {
                UniversityName = request.UniversityName,
                Address = request.Address,
                Phone = request.Phone,
                CreateDate = new DateTime(),
                ModifyDate = new DateTime(),
                DeletedDate = null
            };

            _universityRepository.Add(university);

            return await _universityRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Successful" : "Failed";
        }
    }
}
