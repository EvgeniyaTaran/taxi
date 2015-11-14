using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
	public class OrderedCar
	{
		public int Id { get; set; }

		public int CarId { get; set; }

		[JsonIgnore]
		public Car Car { get; set; }

		public int OrderId { get; set; }
		[JsonIgnore]
		public Order Order { get; set; }

		public OrderType OrderType { get; set; }

		public DateTime StartTime { get; set; }
		public DateTime? EndTime { get; set; }

		//public int RouteId { get; set; }
		//[JsonIgnore]
		//public Route Route { get; set; }
	}
}
