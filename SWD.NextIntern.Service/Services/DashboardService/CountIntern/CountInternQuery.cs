using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.DashboardService.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.DashboardService.CountIntern
{
    public class CountInternQuery : IRequest<ResponseObject<List<CountInternDto>>>, IQuery
    {
    }
}
