using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;

namespace SWD.NextIntern.Service.Services.InternService.GetByUniversityId
{
    public class GetByUniversityIdQueryHandler : IRequestHandler<GetByUniversityIdQuery, ResponseObject<List<InternDto>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetByUniversityIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<List<InternDto>>> Handle(GetByUniversityIdQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<User> query) =>
            {
                return query
                .Where(x => x.DeletedDate == null
                && x.UniversityId.ToString().Equals(request.UniversityId))
                .Include(x => x.Campaign)
                .Include(x => x.Mentor);
            };

            var interns = await _userRepository.FindAllAsync(queryOptions, cancellationToken);

            if (interns == null)
            {
                return new ResponseObject<List<InternDto>>(HttpStatusCode.NotFound, $"University with id {request.UniversityId} does not exist!");
            }

            return new ResponseObject<List<InternDto>>(_mapper.Map<List<InternDto>>(interns), HttpStatusCode.OK, "Success!");
        }
    }
}
