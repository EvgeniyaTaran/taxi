using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Taxi.Entities
{
	public class OrderAddress
	{
		public int Id { get; set; }
		public int Num { get; set; }
		public OrderAddressType Type { get; set; }

		public int OrderId { get; set; }
		[JsonIgnore]
		public Order Order { get; set; }

		public int AddressId { get; set; }
		[JsonIgnore]
		public Address Address { get; set; }
	}

	public enum OrderAddressType
	{
		From,
		To,
		Middle
	}
}
