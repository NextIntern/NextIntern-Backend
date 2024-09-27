using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.CampaignService.GetById;
using SWD.NextIntern.Service.Services.CampaignService;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignQuestionService.GetId
{
    public class GetCampaignQuestionByIdQueryHandler : IRequestHandler<GetCampaignQuestionByIdQuery, ResponseObject<CampaignQuestionDto>>
    {
        private readonly ICampaignQuestionRepository _campaignQuestionRepository;
        private readonly IMapper _mapper;

        public GetCampaignQuestionByIdQueryHandler(IMapper mapper, ICampaignQuestionRepository campaignQuestionRepository)
        {
            _mapper = mapper;
            _campaignQuestionRepository = campaignQuestionRepository;
        }

        public async Task<ResponseObject<CampaignQuestionDto>> Handle(GetCampaignQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            var queryOptions = (IQueryable<CampaignQuestion> query) =>
            {
                return query.Include(x => x.Campaign);
            };

            var question = await _campaignQuestionRepository.FindAsync(c => c.CampaignQuestionId.ToString().Equals(request.Id) && c.DeletedDate == null, queryOptions, cancellationToken);
            if (question is null)
            {
                return new ResponseObject<CampaignQuestionDto>(HttpStatusCode.NotFound, $"Campaign Question with id {request.Id} does not exist!");
            }
            return new ResponseObject<CampaignQuestionDto>(_mapper.Map<CampaignQuestionDto>(question), HttpStatusCode.OK, "Success!");
        }
    }
}
