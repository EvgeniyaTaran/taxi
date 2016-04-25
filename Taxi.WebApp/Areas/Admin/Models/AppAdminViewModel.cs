using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taxi.Entities;

namespace Taxi.WebApp.Areas.Admin.Models
{
	public class AppAdminViewModel
	{
		public ICollection<Car> Cars { get; set; }
		public ICollection<CarModel> Models { get; set; }
		public ICollection<CarBrand> Brands { get; set; }
		public ICollection<Cab> Cabs { get; set; }
		public ICollection<Driver> Drivers { get; set; }
	}
}