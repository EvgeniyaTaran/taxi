using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Services.Models
{
	public class SimpleCoords
	{
		public double X { get; set; }
		public double Y { get; set; }

		public SimpleCoords(double x, double y)
		{
			X = x;
			Y = y;
		}
	}
}
