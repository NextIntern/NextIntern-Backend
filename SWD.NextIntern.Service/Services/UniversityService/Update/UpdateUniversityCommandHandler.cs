
using AutoMapper;
using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Linq.Expressions;
using System.Net;

namespace SWD.NextIntern.Service.Services.UniversityService.Update
{
    public class UpdateUniversityCommandHandler : IRequestHandler<UpdateUniversityCommand, ResponseObject<string>>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IUniversityRepository _universityRepository;

        public UpdateUniversityCommandHandler(ICampaignRepository campaignRepository, IUniversityRepository universityRepository)
        {
            _campaignRepository = campaignRepository;
            _universityRepository = universityRepository;
        }

        public async Task<ResponseObject<string>> Handle(UpdateUniversityCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<University, bool>> queryFilter = (University u) => u.UniversityId.ToString().Equals(request.Id) && u.DeletedDate == null;

            if (queryFilter is null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"University with id {request.UniversityId} doest not exist!");
            }

            var university = await _universityRepository.FindAsync(queryFilter, cancellationToken);

            if (university is null) return new ResponseObject<string>(HttpStatusCode.NotFound, $"University with id {request.Id} does not exist!");

            university.UniversityName = request.UniversityName ?? university.UniversityName;
            university.Address = request.Address ?? university.Address;
            university.Phone = request.Phone ?? university.Phone;
            university.ModifyDate = DateTime.Now;

            if (Guid.TryParse(request.UniversityId, out Guid universityId))
            {
                university.UniversityId = universityId;
            }
            else
            {
                university.UniversityId = university.UniversityId;
            }

                _universityRepository.Update(university);

                return await _universityRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
