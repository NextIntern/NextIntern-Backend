
using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Linq.Expressions;
using System.Net;

namespace SWD.NextIntern.Service.Services.InternService.Update
{
    public class UpdateInternCommandHandler : IRequestHandler<UpdateInternCommand, ResponseObject<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICampaignRepository _campaignRepository;
        private readonly IRoleRepository _roleRepository;

        public UpdateInternCommandHandler(IUserRepository userRepository, ICampaignRepository campaignRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _campaignRepository = campaignRepository;
            _roleRepository = roleRepository;
        }

        public async Task<ResponseObject<string>> Handle(UpdateInternCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<User, bool>> queryFilter = (User u) => u.UserId.ToString().Equals(request.Id) && u.DeletedDate == null;
            Expression<Func<Campaign, bool>> queryFilter1 = (!request.CampaignId.IsNullOrEmpty()) ? (Campaign c) => c.CampaignId.ToString().Equals(request.CampaignId) && c.DeletedDate == null : null;
            Expression<Func<Role, bool>> queryFilter2 = (!request.RoleName.IsNullOrEmpty()) ? (Role r) => r.RoleName.Equals(request.RoleName) && r.DeletedDate == null : null;
            Expression<Func<User, bool>> queryFilter3 = (!request.MenterUsername.IsNullOrEmpty()) ? (User u2) => u2.Username.Equals(request.MenterUsername) && u2.DeletedDate == null : null;

            if (queryFilter is null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"User with id {request.Id}doesnt not exist!");
            }         

            if (!request.CampaignId.IsNullOrEmpty() && queryFilter1 is null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Campaign with id {request.CampaignId}doesnt not exist!");
            }                

            if (!request.RoleName.IsNullOrEmpty() && queryFilter2 is null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Role with name {request.RoleName}doesnt not exist!");
            }

            if (!request.MenterUsername.IsNullOrEmpty() && queryFilter3 is null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"MentorUsername with name {request.MenterUsername}doesnt not exist!");
            }

            var user = await _userRepository.FindAsync(queryFilter, cancellationToken);
            var campaign = !request.CampaignId.IsNullOrEmpty() ? await _campaignRepository.FindAsync(queryFilter1, cancellationToken) : null;
            var role = !request.RoleName.IsNullOrEmpty() ? await _roleRepository.FindAsync(queryFilter2, cancellationToken) : null;
            var mentor = !request.MenterUsername.IsNullOrEmpty() ? await _userRepository.FindAsync(queryFilter3, cancellationToken) : null;

            if (user is null) return new ResponseObject<string>(HttpStatusCode.NotFound, $"User with id {request.Id}does not exist!");
            if (!request.CampaignId.IsNullOrEmpty() && campaign is null) return new ResponseObject<string>(HttpStatusCode.NotFound, $"Campaign with id {request.CampaignId}does not exist!");
            if (!request.RoleName.IsNullOrEmpty() && role is null) return new ResponseObject<string>(HttpStatusCode.NotFound, $"Role with name {request.RoleName}does not exist!");
            if (!request.MenterUsername.IsNullOrEmpty() && mentor is null) return new ResponseObject<string>(HttpStatusCode.NotFound, $"MentorUsername with name {request.MenterUsername}does not exist!");

            user.FullName = request.Fullname ?? user.FullName;
            user.Dob = request.Dob ?? user.Dob;
            user.Gender = request.Gender ?? user.Gender;
            user.Telephone = request.Telephone ?? user.Telephone;
            user.Address = request.Address ?? user.Address;
            user.Mentor = mentor ?? user.Mentor;
            user.ModifyDate = DateTime.Now;


            if (request.CampaignId != null && Guid.TryParse(request.CampaignId, out Guid campaignId))
            {
                user.CampaignId = campaignId;
            }
            else
            {
                user.CampaignId = user.CampaignId;
            }

            _userRepository.Update(user);

            return await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
