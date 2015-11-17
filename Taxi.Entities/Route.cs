using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
	public class Route : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }

		[JsonIgnore]
		public virtual ICollection<Address> Addresses { get; set; }

		public int StartAddressId { get; set; }
		[JsonIgnore]
		public Address StartAddress { get; set; }

		public int EndAddressId { get; set; }
		[JsonIgnore]
		public Address EndAddress { get; set; }

		public double GetRouteLength() 
		{
			//TODO: implement route's length calculation
			return 500;
		}
	}
}
