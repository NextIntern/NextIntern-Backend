using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.UniversityService
{
    public class UniversityDto
    {
        public Guid UniversityId { get; set; }

        public int Id { get; set; }

        public string? UniversityName { get; set; }

        public string? address { get; set; }

        public string? phone { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}
