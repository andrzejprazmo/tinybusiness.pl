//-----------------------------------------------------------------------
// <copyright file="IPrincipalExtensions.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Extensions
{
	using System;
	using System.Collections.Generic;
    using System.Security.Principal;
    using System.Text;
    using System.Security;
    using System.Security.Claims;
    using System.Linq;
    using SEE.TinyBusinessMarket.BackEnd.Common.Consts;

    public static class IPrincipalExtensions
    {
        public static Guid? GetCustomerId(this IPrincipal user)
        {
            var claimsIdentity = user.Identity as ClaimsIdentity;
            string companyId = claimsIdentity.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault();
            Guid result = Guid.Empty;
            if(Guid.TryParse(companyId, out result))
            {
                return result;
            }
            return null;
        }

    }
}
