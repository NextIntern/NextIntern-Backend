using MediatR;
using Microsoft.IdentityModel.Tokens;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignQuestionService.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignQuestionResponseService.Update
{
    public class UpdateCampaignQuestionResponseCommandHandler : IRequestHandler<UpdateCampaignQuestionResponseCommand, ResponseObject<string>>
    {
        private readonly ICampaignQuestionRepository _campaignQuestionRepository;
        private readonly ICampaignQuestionResponseRepository _campaignQuestionResponseRepository;
        private readonly IUserRepository _userRepository;

        public UpdateCampaignQuestionResponseCommandHandler(ICampaignQuestionRepository campaignQuestionRepository, ICampaignQuestionResponseRepository campaignQuestionResponseRepository, IUserRepository userRepository)
        {
            _campaignQuestionRepository = campaignQuestionRepository;
            _campaignQuestionResponseRepository = campaignQuestionResponseRepository;
            _userRepository = userRepository;
        }

        public async Task<ResponseObject<string>> Handle(UpdateCampaignQuestionResponseCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<CampaignQuestionResponse, bool>> queryFilter = (CampaignQuestionResponse r) => r.CampaignQuestionResponseId.ToString().Equals(request.CampaignQuestionResponseId) && r.DeletedDate == null;
            Expression<Func<User, bool>> queryFilter1 = (!request.InternId.IsNullOrEmpty()) ? (User u) => u.UserId.ToString().Equals(request.InternId) && u.DeletedDate == null : null;
            Expression<Func<CampaignQuestion, bool>> queryFilter2 = (!request.InternId.IsNullOrEmpty()) ? (CampaignQuestion q) => q.CampaignQuestionId.ToString().Equals(request.CampaignQuestionId) && q.DeletedDate == null : null;

            if (queryFilter is null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"CampaignQuestionResponse with id {request.CampaignQuestionResponseId} does not exist!");
            }

            if (!request.InternId.IsNullOrEmpty() && queryFilter1 is null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Intern with id {request.InternId} does not exist!");
            }

            if (!request.CampaignQuestionId.IsNullOrEmpty() && queryFilter2 is null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"CampaignQuestion with id {request.CampaignQuestionId} does not exist!");
            }

            var response = await _campaignQuestionResponseRepository.FindAsync(queryFilter, cancellationToken);
            var user = !request.InternId.IsNullOrEmpty() ? await _userRepository.FindAsync(queryFilter1, cancellationToken) : null;
            var question = !request.CampaignQuestionId.IsNullOrEmpty() ? await _campaignQuestionRepository.FindAsync(queryFilter2, cancellationToken) : null;

            if (response is null) return new ResponseObject<string>(HttpStatusCode.NotFound, $"CampaignQuestionResponse with id {request.CampaignQuestionResponseId}does not exist!");
            if (!request.InternId.IsNullOrEmpty() && user is null) return new ResponseObject<string>(HttpStatusCode.NotFound, $"Intern with id {request.InternId}does not exist!");
            if (!request.CampaignQuestionId.IsNullOrEmpty() && question is null) return new ResponseObject<string>(HttpStatusCode.NotFound, $"CampaignQuestion with id {request.CampaignQuestionId}does not exist!");

            response.Response = request.Response;
            response.Rating = request.Rating;


            if (request.CampaignQuestionId != null && Guid.TryParse(request.CampaignQuestionId, out Guid campaignQuestionResponseId))
            {
                response.CampaignQuestionId = campaignQuestionResponseId;
            }
            else
            {
                response.CampaignQuestionId = response.CampaignQuestionId;
            }

            _campaignQuestionResponseRepository.Update(response);

            return await _campaignQuestionRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
