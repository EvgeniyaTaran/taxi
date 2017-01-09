using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taxi.Entities;

namespace Taxi.WebApp.Models
{
	public class WeddingsViewModel : LayoutViewModel
	{
		public List<Car> WeddingCars { get; set; }
	}
}