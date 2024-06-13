using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.EvaluationFormService.GetById
{
    public class GetEvaluationFormByIdQuery : IRequest<ResponseObject<EvaluationFormDto?>>, IQuery
    {
        public string Id { get; set; }

        public GetEvaluationFormByIdQuery(string id)
        {
            Id = id;
        }
    }
}
