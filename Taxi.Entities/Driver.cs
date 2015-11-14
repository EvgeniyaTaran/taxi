using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
    public class Driver: WebUser
    {
		//[JsonIgnore]
		//public ICollection<Car> ActiveCars 
		//{ 
		//	get 
		//	{
		//		return CarsHistory
		//			.Where(ch => ch.DriverId == Id && ch.IsActive).Select(cd => cd.Car).ToList();
		//	}
		//}

		//[JsonIgnore]
		//public Car ActiveCar
		//{
		//	get
		//	{
		//		return CarsHistory.FirstOrDefault(ch => ch.DriverId == Id && ch.IsActive && ch.IsMain) != null 
		//			? CarsHistory.FirstOrDefault(ch => ch.DriverId == Id && ch.IsActive && ch.IsMain).Car
		//			: null;
		//	}
		//}

		[JsonIgnore]
		public ICollection<Cab> CarsHistory { get; set; }

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Driver> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}
    }
}
