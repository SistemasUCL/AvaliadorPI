using System;
using System.Linq;
using System.Security.Claims;

namespace AvaliadorPI.API
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            return new Guid(principal.Claims.FirstOrDefault(x => x.Type == "sub")?.Value);
        }
    }
}
