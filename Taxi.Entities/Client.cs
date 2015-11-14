using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
	public class Client: IClient
	{
		public string Id { get; set; }
		[JsonIgnore]
		public ICollection<Order> Orders { get; set; }
	}
}
