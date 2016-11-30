using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
    public class Car : IEntity
    {
        public int Id { get; set; }
		public int Num { get; set; }

		public BaseLocale Ru { get; set; }
		public BaseLocale En { get; set; }
		public string Number { get; set; }

		public bool IsActive { get; set; }

		public int CarModelId { get; set; }
		[JsonIgnore]
        public CarModel CarModel { get; set; }

        public TaxiClass TaxiType { get; set; }

	    public string OwnerId { get; set; }
	    [JsonIgnore]
		public Driver Owner { get; set; }

	    public bool IsForMarrige { get; set; }

	    //public int CarTypeId { get; set; }
		//[JsonIgnore]
		//public CarType Type { get; set; }

        public int? TechDataId { get; set; }
		[JsonIgnore]
		public TechData TechData { get; set; }

		[JsonIgnore]
		public ICollection<CarPhoto> Photos { get; set; }

		//[JsonIgnore]
		//public ICollection<Driver> ActiveDrivers 
		//{
		//	get 
		//	{ 
		//		return DriversHistory
		//				.Where(dh => dh.CarId == Id && dh.IsActive)
		//				.Select(dh => dh.Driver)
		//				.ToList(); 
		//	}
		//}

		//[JsonIgnore]
		//public Driver ActiveDriver
		//{
		//	get
		//	{
		//		return DriversHistory.FirstOrDefault(dh => dh.CarId == Id && dh.IsActive && dh.IsMain) != null
		//			? DriversHistory.FirstOrDefault(dh => dh.CarId == Id && dh.IsActive && dh.IsMain).Driver
		//			: null;
		//	}
		//}

		[JsonIgnore]
		public ICollection<Cab> Cabs { get; set; }

		//[JsonIgnore]
		//public ICollection<Driver> Drivers 
		//{
		//	get { return DriversHistory.Select(dh => dh.Driver).ToList(); }
		//}

		public CarColor Color { get; set; }
		
		public Car() 
		{
			Ru = new BaseLocale();
			En = new BaseLocale();
		}
    }

	public enum CarColor 
	{
		Black,
		White,
		Green,
		Red,
		Blue,
		Gray,
		Metallic,
		Yellow,
		Pink,
		Orange
	}

    public enum TaxiClass
    {
        Econom,
        Comfort
    }
}
