using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
	public class RoutePoint: IEntity
	{
		public int Id { get; set; }

		public int RouteId { get; set; }
		[JsonIgnore]
		public Route Route { get; set; }

		public int AddressId { get; set; }
		[JsonIgnore]
		public Address Address { get; set; }

		public bool IsStart { get; set; }
		public bool IsEnd { get; set; }
	}
}
