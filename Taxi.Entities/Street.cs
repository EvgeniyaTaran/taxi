using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
	public class Street: IEntity
	{
		public int Id { get; set; }
		public int Num { get; set; }

		public string StreetName { get; set; }
		public StreetType Type { get; set; }
	}

	public enum StreetType
	{
		Street,
		Alley,
		Avenue,
		BlindAlley
	}
}
