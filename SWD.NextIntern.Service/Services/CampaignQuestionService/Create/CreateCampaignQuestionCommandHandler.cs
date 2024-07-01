using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.InternEvaluationCriteriaService.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignQuestionService.Create
{
    public class CreateCampaignQuestionCommandHandler : IRequestHandler<CreateCampaignQuestionCommand, ResponseObject<string>>
    {
        private readonly ICampaignQuestionRepository _campaignQuestionRepository;
        private readonly ICampaignRepository _campaignRepository;

        public CreateCampaignQuestionCommandHandler(ICampaignQuestionRepository campaignQuestionRepository, ICampaignRepository campaignRepository)
        {
            _campaignQuestionRepository = campaignQuestionRepository;
            _campaignRepository = campaignRepository;
        }

        public async Task<ResponseObject<string>> Handle(CreateCampaignQuestionCommand request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignRepository.FindAsync(c => c.CampaignId.ToString().Equals(request.CampaignId) && c.DeletedDate == null);

            if (campaign == null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Campaign with id {request.CampaignId} does not exist!");
            }

            var campaignQuestion = new CampaignQuestion
            {
                CampaignId = Guid.Parse(request.CampaignId),
                Question = request.CampaignQuestion,
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.Now,
            };

            _campaignQuestionRepository.Add(campaignQuestion);

            return await _campaignQuestionRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.Created, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
