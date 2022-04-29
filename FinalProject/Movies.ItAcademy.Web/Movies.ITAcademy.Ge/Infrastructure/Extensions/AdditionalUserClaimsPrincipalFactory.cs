using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MovieManagement.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Movies.ITAcademy.Ge.Infrastructure.Extensions
{
  
	public class AdditionalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
	{
		public AdditionalUserClaimsPrincipalFactory(
			UserManager<User> userManager,
			IOptions<IdentityOptions> optionsAccessor)
			: base(userManager, optionsAccessor) { }
		protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
		{
			var identity = await base.GenerateClaimsAsync(user);
			identity.AddClaim(new Claim("UserName", user.UserName));
			identity.AddClaim(new Claim("LastName", user.LastName));
			identity.AddClaim(new Claim("FirstName", user.FirstName));
			identity.AddClaim(new Claim("Id", user.Id));
			return identity;
		}
	}
}
