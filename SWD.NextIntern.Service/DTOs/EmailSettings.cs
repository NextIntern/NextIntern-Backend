using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextIntern.Domain.DTOs
{
    public class EmailSettings
    {
        public string ApiBaseUri { get; set; }
        public string ApiKey { get; set; }
        public string Domain { get; set; }
    }
}
