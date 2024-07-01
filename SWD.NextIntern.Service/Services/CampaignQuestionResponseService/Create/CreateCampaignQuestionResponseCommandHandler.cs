using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignQuestionService.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignQuestionResponseService.Create
{
    public class CreateCampaignQuestionResponseCommandHandler : IRequestHandler<CreateCampaignQuestionResponseCommand, ResponseObject<string>>
    {
        private readonly ICampaignQuestionRepository _campaignQuestionRepository;
        private readonly ICampaignQuestionResponseRepository _campaignQuestionResponseRepository;
        private readonly IUserRepository _userRepository;

        public CreateCampaignQuestionResponseCommandHandler(ICampaignQuestionRepository campaignQuestionRepository, ICampaignQuestionResponseRepository campaignQuestionResponseRepository, IUserRepository userRepository)
        {
            _campaignQuestionRepository = campaignQuestionRepository;
            _campaignQuestionResponseRepository = campaignQuestionResponseRepository;
            _userRepository = userRepository;
        }

        public async Task<ResponseObject<string>> Handle(CreateCampaignQuestionResponseCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(u => u.UserId.ToString().Equals(request.InternId) && u.DeletedDate == null);

            if (user == null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Intern with id {request.InternId} does not exist!");
            }

            var question = await _campaignQuestionRepository.FindAsync(q => q.CampaignQuestionId.ToString().Equals(request.CampaignQuestionId) && q.DeletedDate == null);

            if (question == null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"CampaignQuestion with id {request.CampaignQuestionId} does not exist!");
            }

            var response = new CampaignQuestionResponse
            {
                CampaignQuestionId = Guid.Parse(request.CampaignQuestionId),
                InternId = Guid.Parse(request.InternId),
                Response = request.Response,
                Rating = request.Rating,
            };

            _campaignQuestionResponseRepository.Add(response);

            return await _campaignQuestionResponseRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.Created, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
