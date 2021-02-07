﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.WebSiteService.SiteAuthorization
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context;
        }
        public int GetUserId()
        {
            var nameId = _context.HttpContext.User.FindFirst("id");

            return nameId != null ? Convert.ToInt32(nameId.Value) : 0;
        }

        public string GetUserName()
        {
            return _context.HttpContext.User.FindFirst("name")?.Value;
        }
    }
}
