using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.DashboardService.Items
{
    public class ItemsDto
    {
        public string? Title {  get; set; }
        public int? Total { get; set; }
        public decimal? Percentage {get; set; }
        public bool? IsIncrease {  get; set; }
    }
}
