
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Linq.Expressions;
using System.Net;

namespace SWD.NextIntern.Service.Services.InternService.GetById
{
    public class GetInternByIdQueryHandler : IRequestHandler<GetInternByIdQuery, ResponseObject<InternDto?>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetInternByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<InternDto?>> Handle(GetInternByIdQuery request, CancellationToken cancellationToken)
        {
            //Expression<Func<User, bool>> queryFilter = (User f) => f.UserId.ToString().Equals(request.Id) && f.DeletedDate == null;

            var queryOptions = (IQueryable<User> query) =>
            {
                return query
                .Include(x => x.Campaign)
                .Include(x => x.Mentor)
                .Where(x => x.DeletedDate == null); ;
            };

            var intern = await _userRepository.FindAsync(queryOptions, cancellationToken);

            if (intern == null)
            {
                return new ResponseObject<InternDto?>(HttpStatusCode.NotFound, $"Intern with id {request.Id} doesnt not exist!");
            }

            return new ResponseObject<InternDto?>(_mapper.Map<InternDto>(intern), HttpStatusCode.OK, "success!");
        }
    }
}
