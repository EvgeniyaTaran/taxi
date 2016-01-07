using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
	public class Country: IEntity
	{
		public int Id { get; set; }
		public int Num { get; set; }
		public BaseLocale Ru { get; set; }
		public BaseLocale En { get; set; }
		public string PhotoPath { get; set; }
		[JsonIgnore]
		public ICollection<CarBrand> Brands { get; set; }

		public Country() 
		{
			Ru = new BaseLocale();
			En = new BaseLocale();
		}
	}

	[ComplexType]
	public class CountryLocale : BaseLocale
	{
		public string Name { get; set; }
	}

	[ComplexType]
	public class BaseLocale
	{
		public string Name { get; set; }
		public string ShortName { get; set; }
		public string Description { get; set; }
	}
}
