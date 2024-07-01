using MediatR;
using Microsoft.IdentityModel.Tokens;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.InternEvaluationCriteriaService.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignQuestionService.Update
{
    public class UpdateCampaignQuestionCommandHandler : IRequestHandler<UpdateCampaignQuestionCommand, ResponseObject<string>>
    {
        private readonly ICampaignQuestionRepository _campaignQuestionRepository;
        private readonly ICampaignRepository _campaignRepository;

        public UpdateCampaignQuestionCommandHandler(ICampaignQuestionRepository campaignQuestionRepository, ICampaignRepository campaignRepository)
        {
            _campaignQuestionRepository = campaignQuestionRepository;
            _campaignRepository = campaignRepository;
        }

        public async Task<ResponseObject<string>> Handle(UpdateCampaignQuestionCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<CampaignQuestion, bool>> queryFilter = (CampaignQuestion q) => q.CampaignQuestionId.ToString().Equals(request.CampaignQuestionId) && q.DeletedDate == null;
            Expression<Func<Campaign, bool>> queryFilter1 = (!request.CampaignId.IsNullOrEmpty()) ? (Campaign c) => c.CampaignId.ToString().Equals(request.CampaignId) && c.DeletedDate == null : null;

            if (queryFilter is null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"CampaignQuestion with id {request.CampaignQuestionId} does not exist!");
            }

            if (!request.CampaignId.IsNullOrEmpty() && queryFilter1 is null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Campaign with id {request.CampaignId} does not exist!");
            }

            var question = await _campaignQuestionRepository.FindAsync(queryFilter, cancellationToken);
            var campaign = !request.CampaignId.IsNullOrEmpty() ? await _campaignRepository.FindAsync(queryFilter1, cancellationToken) : null;

            if (question is null) return new ResponseObject<string>(HttpStatusCode.NotFound, $"CampaignQuestion with id {request.CampaignQuestionId}does not exist!");
            if (!request.CampaignId.IsNullOrEmpty() && campaign is null) return new ResponseObject<string>(HttpStatusCode.NotFound, $"Campaign with id {request.CampaignId}does not exist!");

            question.Question = request.CampaignQuestion;
            question.ModifyDate = request.ModifyDate;


            if (request.CampaignId != null && Guid.TryParse(request.CampaignId, out Guid internEvaluationId))
            {
                question.CampaignId = internEvaluationId;
            }
            else
            {
                question.CampaignId = question.CampaignId;
            }

            _campaignQuestionRepository.Update(question);

            return await _campaignQuestionRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
