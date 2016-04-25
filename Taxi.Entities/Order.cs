using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

		public string ClientComment { get; set; }
		public string DriverComment { get; set; }
		public string AdminComment { get; set; }

		public string ClientId { get; set; }
		[JsonIgnore]
		public WebUser Client { get; set; }
		public int CabId { get; set; }
		[JsonIgnore]
		public Cab Cab { get; set; }

		public DateTime Date { get; set; }

		public Order()
		{
			Date = DateTime.Now;
			Status = OrderStatus.Draft;
		}

		public Order(WebUser client)
		{
			ClientId = client.Id;
			Date = DateTime.Now;
			Status = OrderStatus.Draft;
		}

		public OrderStatus Status { get; set; }
    }

	public enum OrderStatus
	{
		Draft,
		AtWork,
		Active,
		Closed
	}
}
