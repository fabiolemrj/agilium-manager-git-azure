﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace agilium.api.business.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        Guid GetUserId();
        string GetUserEmail();
        bool IsAuthenticated();
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
