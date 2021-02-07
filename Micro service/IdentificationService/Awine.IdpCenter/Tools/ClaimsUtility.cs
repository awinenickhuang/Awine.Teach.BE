using Awine.IdpCenter.Entities;
using IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Awine.IdpCenter.Tools
{
    /// <summary>
    /// Claims工具类
    /// </summary>
    public static class ClaimsUtility
    {
        public static IEnumerable<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
                new Claim(JwtClaimTypes.PreferredUserName, user.UserName),
            };
            return claims;
        }
    }
}
