using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
	public class OrderRequest: IEntity
	{
		public int Id { get; set; }
		public WaitingPeriod Period { get; set; }
		public ServiceClass Class { get; set; }

		public Address StartPoint { get; set; }
		public Address EndPoint { get; set; }
		public List<Address> RoutePoints { get; set; }
	}

	public enum WaitingPeriod
	{
		About_15,
		About_30,
		About_60,
		Custom
	}

	public static class WaitingPeriodExtensions
	{
		public static int GetTime(this WaitingPeriod period)
		{
			if (period == WaitingPeriod.Custom)
				return 0;
			switch (period)
			{
				case WaitingPeriod.About_15:
					return 15;
				case WaitingPeriod.About_30:
					return 30;
				case WaitingPeriod.About_60:
					return 60;
				default:
					return 0;
			}
		}
	}

	public enum ServiceClass
	{
		Econom,
		Business,
		All
	}
}
