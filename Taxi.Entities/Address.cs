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
    [ComplexType]
	public class Address
	{
        public string Name { get; set; }
        public GeoCoordinates Coords { get; set; }

        public Address()
        {
            Coords = new GeoCoordinates();
        }
	}

    [ComplexType]
    public class GeoCoordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
