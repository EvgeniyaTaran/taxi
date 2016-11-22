using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taxi.Entities;
using System.Device.Location;

namespace Taxi.WebApp.Models.Dtos
{
    public class OrderDto
    {
        public string AddressFrom { get; set; }
        public string AddressTo { get; set; }
        public bool ChildSeat { get; set; }
        public bool Animals { get; set; }
        public bool Nonsmoking { get; set; }
        public TaxiClass TaxiClass{ get; set;}
        public string GeoCoordinatesFromLat { get; set; }
        public string GeoCoordinatesFromLng { get; set; }
        public string GeoCoordinatesToLat { get; set; }
        public string GeoCoordinatesToLng { get; set; }
	    public string ClientId { get; set; }
    }

    
}