using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
	public class Country: IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		[JsonIgnore]
		public ICollection<CarBrand> Brands { get; set; }
	}
}
