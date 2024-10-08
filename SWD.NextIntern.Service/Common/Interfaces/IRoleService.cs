﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Common.Interfaces
{
    public interface IRoleService
    {
        Task<string> CheckToken(string token);
        Task<string> GetAllRoles();
    }
}
