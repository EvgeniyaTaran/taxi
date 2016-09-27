using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taxi.WebApp.Models.Dtos
{
	public class DriverLogInDto
	{
		public string DriverId { get; set; }
		public int CarId { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}