using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignService.Delete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignQuestionService.Delete
{
    public class DeleteCampaignQuestionCommandHandler : IRequestHandler<DeleteCampaignQuestionCommand, ResponseObject<string>>
    {
        private readonly ICampaignQuestionRepository _campaignQuestionRepository;
        private readonly ICampaignQuestionResponseRepository _campaignQuestionResponseRepository;
        private readonly AppDbContext _dbContext;

        public DeleteCampaignQuestionCommandHandler(ICampaignQuestionRepository campaignQuestionRepository, AppDbContext dbContext, ICampaignQuestionResponseRepository campaignQuestionResponseRepository)
        {
            _campaignQuestionRepository = campaignQuestionRepository;
            _dbContext = dbContext;
            _campaignQuestionResponseRepository = campaignQuestionResponseRepository;
        }

        public async Task<ResponseObject<string>> Handle(DeleteCampaignQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _campaignQuestionRepository.FindAsync(c => c.CampaignQuestionId.ToString().Equals(request.Id) && c.DeletedDate == null, cancellationToken);
            
            if (question == null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Campaign Question with id {request.Id} does not exist!");
            }

            var responseList = _dbContext.CampaignQuestionResponses
               .Include(s => s.CampaignQuestion)
               .Where(s => s.DeletedDate == null && s.CampaignQuestionId.ToString().Equals(request.Id))
               .ToList();

            foreach (var item in responseList)
            {
                item.DeletedDate = DateTime.Now;
                await _campaignQuestionResponseRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }

            question.DeletedDate = DateTime.Now;

            return await _campaignQuestionRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
