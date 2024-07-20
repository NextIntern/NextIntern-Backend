using AutoMapper;
using SWD.NextIntern.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.DashboardService.MostIntern
{
    public class MostInternDto
    {
        public Guid? InternId { get; set; }
        public string? Name { get; set; }
        public string? InternMail { get; set; }
        public string? FormCriteriaName { get; set; }
        public int? Score { get; set; }
    }
}
