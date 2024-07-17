using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;


namespace SWD.NextIntern.Service.Services.UniversityService.Create
{
    public class CreateUniversityCommandHandler : IRequestHandler<CreateUniversityCommand, ResponseObject<string>>
    {
        private readonly IUniversityRepository _universityRepository;

        public CreateUniversityCommandHandler(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        public async Task<ResponseObject<string>> Handle(CreateUniversityCommand request, CancellationToken cancellationToken)
        {
            var existUniversity = await _universityRepository.FindAsync(u => u.UniversityName.Equals(request.UniversityName), cancellationToken);

            if (existUniversity != null)
            {
                return new ResponseObject<string>(HttpStatusCode.BadRequest, $"University with name {request.UniversityName} is exist!");
            }

            var university = new University
            {
                UniversityName = request.UniversityName,
                Address = request.Address,
                Phone = request.Phone,
                ImgUrl = request.ImgUrl,
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.Now,
                DeletedDate = null
            };
            _universityRepository.Add(university);

            return await _universityRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.Created, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
