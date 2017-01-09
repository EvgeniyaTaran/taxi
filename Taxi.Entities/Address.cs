using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public GeoCoordinates Coords { get; set; }

		public ICollection<OrderAddress> Orders { get; set; }

		public Address()
        {
            Coords = new GeoCoordinates();
			Orders = new List<OrderAddress>();
        }
	}

    [ComplexType]
    public class GeoCoordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
