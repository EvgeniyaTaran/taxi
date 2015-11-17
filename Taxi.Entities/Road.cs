using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
	public class Road : IEntity
	{
		public int Id { get; set; }

		//public int FirstPointId { get; set; }
		[JsonIgnore]
		public List<RoadPoint> Points { get; set; }

		//public int SecondPointId { get; set; }
		//[JsonIgnore]
		//public RoadPoint SecondPoint { get; set; }

		public double LongSize { get; set; }

		public RoadType Type { get; set; }

		public int FreeValue { get; set; }

		public float PriceKoef { get; set; }
		[JsonIgnore]
		public ICollection<Address> Addresses { get; set; }
	}

	public enum RoadType
	{
		Ground, A, B, C, D
	}
}
