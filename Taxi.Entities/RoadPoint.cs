using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
	public class RoadPoint : IEntity
	{
		public int Id { get; set; }
		public string name { get; set; }

		public GeoCoordinate Point { get; set; }

		[JsonIgnore]
		public virtual ICollection<Road> Roads { get; set; }

		[JsonIgnore]
		public virtual ICollection<Address> Addresses { get; set; }

		public RoadPoint()
		{
			//Roads = new List<Road>();
			//Addresses = new List<Address>();
		}
	}
}
