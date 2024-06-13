using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.UniversityService.GetById
{
    public class GetUniversityByIdQuery : IRequest<ResponseObject<UniversityDto?>>, IQuery
    {
        public string Id { get; set; }

        public GetUniversityByIdQuery(string id)
        {
            Id = id;
        }
    }
}
