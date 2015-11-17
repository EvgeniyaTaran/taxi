using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
	public class OrderAddress: IEntity
	{
		public int Id { get; set; }
		public int OrderRequestId { get; set; }
		[JsonIgnore]
		public OrderRequest OrderRequest { get; set; }

		public int AddressId { get; set; }
		[JsonIgnore]
		public Address Address { get; set; }

		public int Num { get; set; }
		public bool IsStart { get; set; }
		public bool IsEnd { get; set; }
	}
}
