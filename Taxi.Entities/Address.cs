using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
	public class Address: IEntity
	{
		public int Id { get; set; }

		public String StreetName { get; set; }
		public StreetType Type { get; set; }
		public string Number { get; set; }
		public int? Porch { get; set; }
		//public GeoCoordinates
	}

	public enum StreetType 
	{
		Street,
		Alley,
		Avenue,
		BlindAlley
	}
}
