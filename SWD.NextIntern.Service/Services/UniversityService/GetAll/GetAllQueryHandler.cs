using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignService;
using System.Net;

namespace SWD.NextIntern.Service.Services.UniversityService.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, ResponseObject<List<UniversityDto>>>
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(IUniversityRepository universityRepository, IMapper mapper)
        {
            _universityRepository = universityRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<List<UniversityDto>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<University> query) =>
            {
                return query.Include(x => x.Campaigns);
            };

            var universities = await _universityRepository.FindAllAsync(queryOptions, cancellationToken);
            var universityDtos = _mapper.Map<List<UniversityDto>>(universities);
            return new ResponseObject<List<UniversityDto>>(universityDtos, HttpStatusCode.OK, "success!"); ;
        }
    }
}
