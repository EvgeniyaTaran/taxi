using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Entities.Helpers;

namespace Taxi.Entities
{
	public class OrderRequest: IEntity
	{
		public int Id { get; set; }
		public WaitingPeriod Period { get; set; }
		public ServiceClass Class { get; set; }

		[JsonIgnore]
		public string AddressesJson { get; set; }

		[NotMapped]
		private List<Address> _addressesJson;

		[NotMapped]
		public List<Address> Addresses
		{
			get 
			{
				if (_addressesJson != null)
				{
					return _addressesJson;
				}
				return
					_addressesJson =
						JsonConvert.DeserializeObject<List<Address>>(AddressesJson, JsonSettings.Default());
			}
			set 
			{
				_addressesJson = value;
				AddressesJson = JsonConvert.SerializeObject(value, JsonSettings.Default());
			}
		}
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
