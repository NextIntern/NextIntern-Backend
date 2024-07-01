using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignQuestionResponseService.GetAll
{
    public class GetAllQuery : IRequest<ResponseObject<List<CampaignQuestionResponseDto>>>, IQuery
    {
    }
}
