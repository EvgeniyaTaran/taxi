using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
	public class Photo: IEntity
	{
		public int Id { get; set; }
		public int Num { get; set; }
		public string FileName { get; set; }
		public string Description { get; set; }

		public bool IsMain { get; set; }
	}

	public class CarPhoto: Photo
	{
		public int CarId { get; set; }
		[JsonIgnore]
		public Car Car { get; set; }
	}

	public class WebUserPhoto : Photo
	{
		public string WebUserId { get; set; }
		[JsonIgnore]
		public WebUser WebUser { get; set; }
	}

	public class AddressPhoto : Photo
	{
		public int AddressId { get; set; }
		[JsonIgnore]
		public Address Address { get; set; }
	}
}
