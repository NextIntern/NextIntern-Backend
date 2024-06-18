﻿using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.FormCriteriaService.GetAll
{
    public class GetAllQuery : IRequest<ResponseObject<List<FormCriteriaDto>>>, IQuery
    {
    }
}