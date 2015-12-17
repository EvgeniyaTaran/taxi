using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taxi.Entities;

namespace Taxi.WebApp.Models
{
	public class LayoutViewModel
	{
		public LayoutViewModel()
		{
		}

		public IEnumerable<Car> Cars { get; set; }
		public IEnumerable<CarModel> CarModels { get; set; }
		public IEnumerable<CarBrand> CarBrands { get; set; }
	}
}