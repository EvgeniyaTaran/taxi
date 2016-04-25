using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
	public class Cab : IEntity
	{
		public int Id { get; set; }
		public int Num { get; set; }

		public int CarId { get; set; }
		[JsonIgnore]
		public Car Car { get; set; }

		public string DriverId { get; set; }
		[JsonIgnore]
		public Driver Driver { get; set; }

		public bool IsMain { get; set; }

		public DateTime DateStart { get; set; }

		public DateTime? DateStop { get; set; }

		public CabStatus Status { get; set; }

        [JsonIgnore]
		public GeoCoordinates Coords { get; set; }

		[JsonIgnore]
		public bool IsActive { get { return DateStop == null; } }

		[JsonIgnore]
		public bool IsReady { get { return Status == CabStatus.Free; } }

		[JsonIgnore]
		public ICollection<Order> Orders { get; set; }
	}

	public enum CabStatus 
	{
		Free,
		AtWork,
		Busy
	}
}
