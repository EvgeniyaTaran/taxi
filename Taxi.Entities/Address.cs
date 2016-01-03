using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
	public class Address: IEntity
	{
		public int Id { get; set; }
		public int Num { get; set; }
		public int StreetId { get; set; }
		[JsonIgnore]
		public Street Street { get; set; }
		public string Number { get; set; }
		public int? Porch { get; set; }
		public GeoCoordinate Coords { get; set; }
		public int RoadId { get; set; }
		[JsonIgnore]
		public Road Road { get; set; }

		[JsonIgnore]
		public virtual ICollection<RoutePoint> Routes { get; set; }

		[JsonIgnore]
		public virtual ICollection<OrderedAddress> OrderedAddresses { get; set; }

		[JsonIgnore]
		public virtual ICollection<AddressPhoto> Photos { get; set; }
	}
}
