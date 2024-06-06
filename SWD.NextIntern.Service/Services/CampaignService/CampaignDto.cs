using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.CampaignService
{
    public class CampaignDto
    {
        public Guid CampaignId { get; set; }

        public int Id { get; set; }

        public string CampaignName { get; set; } = null!;

        public Guid? UniversityId { get; set; }

        public string? UniversityName { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

    }
}
