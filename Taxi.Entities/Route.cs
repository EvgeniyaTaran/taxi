using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
	public class Route : IEntity
	{
		public int Id { get; set; }
		public int Num { get; set; }
		public string Name { get; set; }

		[JsonIgnore]
		public virtual ICollection<RoutePoint> Addresses { get; set; }

		public double GetRouteLength() 
		{
			//TODO: implement route's length calculation
			return 500;
		}
	}
}
