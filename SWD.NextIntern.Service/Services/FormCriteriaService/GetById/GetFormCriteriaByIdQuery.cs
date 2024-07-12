using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.FormCriteriaService.GetById
{
    public class GetFormCriteriaByIdQuery : IRequest<ResponseObject<FormCriteriaDto?>>, IQuery
    {
        public string Id { get; set; }

        public GetFormCriteriaByIdQuery(string id)
        {
            Id = id;
        }
    }
}
