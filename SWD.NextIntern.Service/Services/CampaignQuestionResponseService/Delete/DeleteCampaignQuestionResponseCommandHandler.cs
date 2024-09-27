using MediatR;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignQuestionService.Delete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignQuestionResponseService.Delete
{
    public class DeleteCampaignQuestionResponseCommandHandler : IRequestHandler<DeleteCampaignQuestionResponseCommand, ResponseObject<string>>
    {
        private readonly ICampaignQuestionResponseRepository _campaignQuestionResponseRepository;

        public DeleteCampaignQuestionResponseCommandHandler(ICampaignQuestionResponseRepository campaignQuestionResponseRepository)
        {
            _campaignQuestionResponseRepository = campaignQuestionResponseRepository;
        }

        public async Task<ResponseObject<string>> Handle(DeleteCampaignQuestionResponseCommand request, CancellationToken cancellationToken)
        {
            var response = await _campaignQuestionResponseRepository.FindAsync(c => c.CampaignQuestionResponseId.ToString().Equals(request.Id) && c.DeletedDate == null, cancellationToken);
            if (response == null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Campaign Question Response with id {request.Id} does not exist!");
            }

            response.DeletedDate = DateTime.Now;

            return await _campaignQuestionResponseRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
