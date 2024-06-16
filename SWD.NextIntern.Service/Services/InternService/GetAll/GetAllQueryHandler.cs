using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignService;
using System.Net;

namespace SWD.NextIntern.Service.Services.InternService.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, ResponseObject<List<InternDto>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<List<InternDto>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<User> query) =>
            {
                return query
                .Include(x => x.Campaign)
                .Include(x => x.Mentor)
                .Where(x => x.DeletedDate == null); ;
            };

            var intern = await _userRepository.FindAllAsync(queryOptions, cancellationToken);
            var internDtos = _mapper.Map<List<InternDto>>(intern);
            return new ResponseObject<List<InternDto>>(internDtos, HttpStatusCode.OK, "success!"); ;
        }
    }
}
