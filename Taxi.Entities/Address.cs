using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
	public class Address
	{
		public int Id { get; set; }
        public string Name { get; set; }
        public GeoCoordinate LatLng { get; set; }
	}
}
