using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
	public class OrderedAddress: IEntity
	{
		public int Id { get; set; }
		public int Num { get; set; }
		public int OrderId { get; set; }
		[JsonIgnore]
		public Order Order { get; set; }

		public int AddressId { get; set; }
		[JsonIgnore]
		public Address Address { get; set; }
		public int WaitInSeconds { get; set; }

		public bool IsStart { get; set; }
		public bool IsEnd { get; set; }
	}
}
