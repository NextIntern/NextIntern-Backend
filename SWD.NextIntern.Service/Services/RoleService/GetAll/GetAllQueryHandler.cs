using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.RoleService;
using System.Net;

namespace SWD.NextIntern.Service.Services.RoleService.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, ResponseObject<List<RoleDto>>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<List<RoleDto>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<Role> query) =>
            {
                return query
                .Include(x => x.Users)
                .Where(x => x.DeletedDate == null); ;
            };

            var roles = await _roleRepository.FindAllAsync(queryOptions, cancellationToken);
            var roleDtos = _mapper.Map<List<RoleDto>>(roles);
            return new ResponseObject<List<RoleDto>>(roleDtos, HttpStatusCode.OK, "success!"); ;
        }
    }
}
