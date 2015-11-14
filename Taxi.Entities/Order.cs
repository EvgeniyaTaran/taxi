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

		public string ClientId { get; set; }
		[JsonIgnore]
		public WebClient Client { get; set; }

		public string ManagerId { get; set; }
		[JsonIgnore]
		public Manager Manager { get; set; }

		[JsonIgnore]
		public virtual ICollection<OrderedCar> Cars { get; set; }

		public DateTime Date { get; set; }

		public Order()
		{
			Date = DateTime.Now;
			Cars = new List<OrderedCar>();
			Status = OrderStatus.Draft;
		}

		public Order(WebUser client)
		{
			ClientId = client.Id;
			Date = DateTime.Now;
			Cars = new List<OrderedCar>();
			Status = OrderStatus.Draft;
		}

		public OrderStatus Status { get; set; }
    }

	public enum OrderType
	{
		Simple, TwoWay, Multiple, ForTime
	}

	public enum OrderStatus
	{
		Draft,
		AtWork,
		Active,
		Closad
	}
}
