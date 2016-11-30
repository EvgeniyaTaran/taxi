using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
	public class WebUser: IdentityUser
	{
		public string FirstName { get; set; }
		public string Surname { get; set; }
		public DateTime BirthDate { get; set; }
		public Sex Sex { get; set; }

		public virtual ICollection<WebUserPhoto> Photos { get; set; }

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<WebUser> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}

		public string GetFullName()
		{
			return $"{FirstName} {Surname}";
		}
	}

	public enum Sex
	{
		Male, 
		Female
	}
}
