
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignService;
using System.Linq.Expressions;
using System.Net;

namespace SWD.NextIntern.Service.Services.UniversityService.GetById
{
    public class GetUniversityByIdQueryHandler : IRequestHandler<GetUniversityByIdQuery, ResponseObject<UniversityDto?>>
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IMapper _mapper;

        public GetUniversityByIdQueryHandler(IUniversityRepository universityRepository, IMapper mapper)
        {
            _universityRepository = universityRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<UniversityDto?>> Handle(GetUniversityByIdQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<University, bool>> queryFilter = (University u) => u.UniversityId.ToString().Equals(request.Id) && u.DeletedDate == null;

            var university = await _universityRepository.FindAsync(queryFilter, cancellationToken);

            if (university == null)
            {
                return new ResponseObject<UniversityDto?>(HttpStatusCode.NotFound, $"University with id {request.Id} doest not exist!");
            }

            return new ResponseObject<UniversityDto?>(_mapper.Map<UniversityDto>(university), HttpStatusCode.OK, "success!");
        }
    }
}
