using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.WebSiteService.SiteAuthorization
{
    public interface IIdentityService
    {
        int GetUserId();

        string GetUserName();
    }
}
