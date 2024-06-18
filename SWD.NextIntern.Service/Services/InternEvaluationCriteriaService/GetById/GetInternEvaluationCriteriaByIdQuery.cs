using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.InternEvaluationCriteriaService.GetById
{
    public class GetInternEvaluationCriteriaByIdQuery : IRequest<ResponseObject<InternEvaluationCriteriaDto?>>, IQuery
    {
        public string Id { get; set; }

        public GetInternEvaluationCriteriaByIdQuery(string id)
        {
            Id = id;
        }
    }
}
