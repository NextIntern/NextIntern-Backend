using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.InternService.GetById
{
    public class GetInternByIdQuery : IRequest<ResponseObject<InternDto?>>, IQuery
    {
        public string Id { get; set; }

        public GetInternByIdQuery(string id)
        {
            Id = id;
        }
    }
}
