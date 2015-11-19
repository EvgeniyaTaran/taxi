using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taxi.Entities;

namespace Taxi.WebApp.Models.Dtos
{
	public class CarDto
	{
		public int ModelId { get; set; }
		//public CarColor Color { get; set; }
		public string Number { get; set; }
		public string Name { get; set; }
	}
}