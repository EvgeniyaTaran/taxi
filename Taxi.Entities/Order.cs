using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
    public class Order: IEntity
    {
		public int Id { get; set; }
		public int Num { get; set; }
        public TaxiClass TaxiType { get; set; }
		public string ClientComment { get; set; }
		public string DriverComment { get; set; }
		public string AdminComment { get; set; }

		public string ClientId { get; set; }
		[JsonIgnore]
		public WebUser Client { get; set; }
		public int? CabId { get; set; }
		[JsonIgnore]
		public Cab Cab { get; set; }

		public DateTime Date { get; set; }

	    public ICollection<OrderAddress> Addresses { get; set; }

	    [NotMapped]
	    public OrderAddress AddressFrom => Addresses.OrderBy(a => a.Num).FirstOrDefault();

		[NotMapped]
		public OrderAddress AddressTo => Addresses.OrderByDescending(a => a.Num).FirstOrDefault();

		public Order()
		{
			Date = DateTime.Now;
			Status = OrderStatus.Draft;
			Addresses = new List<OrderAddress>();
		}

		public Order(WebUser client)
		{
			ClientId = client.Id;
			Date = DateTime.Now;
			Status = OrderStatus.Draft;
			Addresses = new List<OrderAddress>();
		}

		public OrderStatus Status { get; set; }
    }

	public enum OrderStatus
	{
		New,
		Processing,
		Active,
		Closed,
		Draft
	}
}
