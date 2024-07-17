using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.DashboardService.Items;
using SWD.NextIntern.Service.Services.DashboardService.MostIntern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.DashboardService._5MostIntern
{
    public class MostInternQuery : IRequest<ResponseObject<List<MostInternDto>>>, IQuery
    {
    }
}
