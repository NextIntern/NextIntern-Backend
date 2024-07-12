using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignService.GetById
{
    public class GetCampaignByIdQuery : IRequest<ResponseObject<CampaignDto>>, IQuery
    {
        public string Id { get; set; }

        public GetCampaignByIdQuery(string id)
        {
            Id = id;
        }
    }
}
